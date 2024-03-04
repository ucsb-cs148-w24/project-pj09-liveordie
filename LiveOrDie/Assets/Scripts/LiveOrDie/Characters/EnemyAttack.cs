using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Player player1, player2;
    private Player currentTarget;
    private bool isAttacking = false;
    private Enemy enemy;

    void OnEnable()
    {
        // Assuming player tags are "Player1" and "Player2"
        player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>();
        player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>();

        enemy = GetComponent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player1.isDead || player2.isDead) return;
        if (collision.gameObject.CompareTag("Player1"))
        {
            currentTarget = player1;
        }
        else if (collision.gameObject.CompareTag("Player2"))
        {
            currentTarget = player2;
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
            Debug.Log(currentTarget.GetComponentInChildren<CharacterHealth>().player.whichPlayer);
            currentTarget.GetComponentInChildren<CharacterHealth>().DecreaseHealth(enemy.damage);
            yield return new WaitForSeconds(1f);
        }
    }

    void OnDestroy()
    {
        StopCoroutine(AttackPlayer());
    }
}