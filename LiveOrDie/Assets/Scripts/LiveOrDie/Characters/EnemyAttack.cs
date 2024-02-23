using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // This will destroy the enemy if it comes into contact with the player. 
    //Will need to add to the main player script and completed healthbar/UI 
    // private void OnCollisionEnter2D(Collision2D col) {
    //     if(col.gameObject.tag == "Bullet") {
    //         // col.gameObject.GetComponent<PlayerScript>().hurt();
    //         Destroy(this.gameObject);
    //     }
    // }
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
            Debug.Log("Attacking");
            // Decrease player's health by 1 each second
            currentTarget.GetComponent<CharacterHealth>().DecreaseHealth();
            yield return new WaitForSeconds(0.1f);
        }
    }

    void OnDestroy()
    {
        StopCoroutine(AttackPlayer());
    }
}