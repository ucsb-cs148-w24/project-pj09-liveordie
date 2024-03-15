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

    public Animator myAnim;

    void OnEnable()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>();
        player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>();
        canAttack = true;
        enemy = GetComponent<Enemy>();
        myAnim = GetComponent<Animator>();
        myAnim.Play("move_animation");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!player1 || !player2 || player1.isDead || player2.isDead) return;
        if (collision.gameObject.CompareTag("Player1"))
            currentTarget = player1;
        else if (collision.gameObject.CompareTag("Player2"))
            currentTarget = player2;

        if (currentTarget != null && canAttack )
        {



            currentTarget.GetComponentInChildren<CharacterHealth>().DecreaseHealth(enemy.damage);

            myAnim.Play("attack_animation");
            AudioMgr.Instance.PlayAudio(enemy.EnemyAttackAudioName, false);

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