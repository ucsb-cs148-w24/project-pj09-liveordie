using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Transform player1Transform;
    private Transform player2Transform;
    private Transform currentTarget;
    private bool isAttacking = false;

    void Start()
    {
        // Assuming player tags are "Player1" and "Player2"
        player1Transform = GameObject.FindGameObjectWithTag("Player1").transform;
        player2Transform = GameObject.FindGameObjectWithTag("Player2").transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player1"))
        {
            currentTarget = player1Transform;
        }
        else if (collision.gameObject.CompareTag("Player2"))
        {
            currentTarget = player2Transform;
        }

        if (currentTarget != null)
        {
            isAttacking = true;
            StartCoroutine(AttackPlayer());
        }
    }

    IEnumerator AttackPlayer()
    {
        while (isAttacking && currentTarget != null)
        {
            yield return new WaitForSeconds(0.1f);
        }
    }

    void OnDestroy()
    {
        StopCoroutine(AttackPlayer());
    }
}