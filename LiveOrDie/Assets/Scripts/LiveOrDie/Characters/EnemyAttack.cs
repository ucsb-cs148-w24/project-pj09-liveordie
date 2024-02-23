using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    float damage;
    float damagePerSecond = 1f;
    private bool isFollowingPlayer = false;
    private Transform playerTransform;
    void Update()
    {
        if (isFollowingPlayer)
        {
            // calculate hp of players and update
            damage = damagePerSecond * Time.deltaTime;
            playerTransform.GetComponent<PlayerHealth>().DecreaseHealth(damage);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isFollowingPlayer = true;
        }
    }

    // This will destroy the enemy if it comes into contact with the player. 
    //Will need to add to the main player script and completed healthbar/UI 
        private void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "Bullet") {
            // col.gameObject.GetComponent<PlayerScript>().hurt();
            Destroy(this.gameObject);
        }
    }
}
