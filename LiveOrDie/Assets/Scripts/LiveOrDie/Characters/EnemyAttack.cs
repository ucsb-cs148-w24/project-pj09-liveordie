using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // This will destroy the enemy if it comes into contact with the player. 
    //Will need to add to the main player script and completed healthbar/UI 
    private void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "Player") {
            // col.gameObject.GetComponent<PlayerScript>().hurt();
            Destroy(this.gameObject);
        }
    }
}
