using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask raycastLayerMask;
    [SerializeField] private Bomb bombPrefab;
    [SerializeField] private GameObject sprite;
    private bool isMoving = false;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bombPrefab,transform.position,Quaternion.identity);
        }
        
        if (isMoving)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            MovePlayerTo(Vector2.left);
            sprite.transform.DORotate(new Vector3(0, 0, 0), 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            MovePlayerTo(Vector2.right);
            sprite.transform.DORotate(new Vector3(0, 180, 0), 0.2f);
            //sprite.transform.rotation = Quaternion.Euler(0,180,0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            MovePlayerTo(Vector2.up);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            MovePlayerTo(Vector2.down);
        }
    }
    private void MovePlayerTo(Vector2 dir)
    {
        if (Raycast(dir))
        {
            return;
        }

        isMoving = true;
        var pos = (Vector2) transform.position + dir;
        transform.DOMove(pos, 0.5f).OnComplete(() =>
        {
            isMoving = false;
        });
    }
    private bool Raycast(Vector2 dir)
    {
        var hit = Physics2D.Raycast(transform.position, dir, 1f,raycastLayerMask);
        return hit.collider != null;
    }

    // private GameObject RayCastFromCamera()
    // {
    //     var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero);
    //     return hit.collider != null ? hit.collider.gameObject : null;
    // }

    private void OnDestroy()
    {
        SceneManager.LoadScene(0);
    }
}
