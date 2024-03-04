using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Player player1, player2;
    private Player currentTarget;
    // private bool isAttacking = false;
    private float attackCoolDownTime = 0.2f;
    private float curAttackCoolDownTime = 0.2f;
    private bool canAttack = true;
    private Enemy enemy;

    void OnEnable()
    {
        // Assuming player tags are "Player1" and "Player2"
        player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>();
        player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>();
        attackCoolDownTime = 0.2f;
        curAttackCoolDownTime = 0.2f;

        enemy = GetComponent<Enemy>();
    }

    private void OnTriggerStay2D(Collider2D collision)
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

        if (currentTarget != null &&  canAttack )
        {
            Debug.Log(currentTarget.GetComponentInChildren<CharacterHealth>().player.whichPlayer);
            currentTarget.GetComponentInChildren<CharacterHealth>().DecreaseHealth(enemy.damage);
            canAttack = false;
            if(this.gameObject.activeSelf) StartCoroutine(AttackCoolDownCoroutine());
        }
    }

    IEnumerator AttackCoolDownCoroutine()
    {
        while (currentTarget != null && curAttackCoolDownTime > 0)
        {
            curAttackCoolDownTime -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        print("reset");
        curAttackCoolDownTime = attackCoolDownTime;
        canAttack = true;
    }

    void OnDisable()
    {
        StopCoroutine(AttackCoolDownCoroutine());
    }
}