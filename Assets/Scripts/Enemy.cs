using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector2 direction = Vector2.down;
    [SerializeField] private LayerMask enemyLayerMask;
    private void Start()
    {
        direction = Vector2.down;
        StartCoroutine(EnemyPatrol());
    }

    private IEnumerator EnemyPatrol()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (Raycast(direction))
            {
                var pos = (Vector2) transform.position + direction;
                transform.DOMove(pos, 0.5f);
            }
            else
            {
                direction *= (-1);
                var pos = (Vector2) transform.position + direction;
                transform.DOMove(pos, 0.5f);
            }
        }
    }
    
    private bool Raycast(Vector2 dir)
    {
        var hit = Physics2D.Raycast(transform.position, dir, 0.7f,enemyLayerMask);
        print(hit.collider == null);
        return hit.collider == null;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Destroy(col.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position,direction);
    }
}