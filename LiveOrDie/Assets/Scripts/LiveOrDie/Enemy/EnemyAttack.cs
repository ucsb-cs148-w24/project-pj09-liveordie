using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Player player1, player2;
    private Player currentTarget;
    private float attackCoolDownTime = 0.2f;
    private bool canAttack = true;
    private Enemy enemy;

    void OnEnable()
    {
        // Assuming player tags are "Player1" and "Player2"
        player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>();
        player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>();
        canAttack = true;
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
            AudioMgr.Instance.PlayAudio("beingCHEWED",true);
            Debug.Log(currentTarget.GetComponentInChildren<CharacterHealth>().player.whichPlayer);
            currentTarget.GetComponentInChildren<CharacterHealth>().DecreaseHealth(enemy.damage);
            canAttack = false;
            if(this.gameObject.activeSelf) StartCoroutine(AttackCoolDownCoroutine());
        }
        currentTarget = null;
    }

    IEnumerator AttackCoolDownCoroutine()
    {
        yield return new WaitForSeconds(attackCoolDownTime);
        canAttack = true;
    }

    void OnDisable()
    {
        StopCoroutine(AttackCoolDownCoroutine());
    }
}