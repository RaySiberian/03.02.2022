using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private LayerMask bombLayerMask;
    [SerializeField] private GameObject FxExplosion;
    private void OnEnable()
    {
        StartCoroutine(WaitForBoom());
    }

    private IEnumerator WaitForBoom()
    {
        yield return new WaitForSeconds(2.5f);
        print("Boom");
        Instantiate(FxExplosion, (Vector2)transform.position + Vector2.down, Quaternion.identity);
        var colliders = Physics2D.OverlapCircleAll(transform.position,1f,bombLayerMask);
        foreach (var coll in colliders)
        {
            Destroy(coll.gameObject);
        }
        Destroy(gameObject);
    }
}