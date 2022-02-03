using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector2 direction;
    [SerializeField] private LayerMask enemyLayerMask;
    private void Start()
    {
        StartCoroutine(EnemyPatrol());
    }

    private IEnumerator EnemyPatrol()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (Raycast(direction))
            {
                Move();
            }
            else
            {
                direction *= (-1);
                Move();
            }
        }
    }

    private void Move()
    {
        var pos = (Vector2) transform.position + direction;
        transform.DOMove(pos, 1f);
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