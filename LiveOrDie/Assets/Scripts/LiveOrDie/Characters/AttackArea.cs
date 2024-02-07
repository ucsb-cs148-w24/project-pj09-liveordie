using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // This will detect if an enemy is within the  area when the attack button in the player script is pressed.
    // The enemy will be destroyed. We will need to update this function with a "reward" for destroying an enemy.
    private void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag == "Enemy") {
            Destroy(col.gameObject);
        }
    }
}
