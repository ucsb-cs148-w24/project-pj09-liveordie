using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{

    // private GameObject attackArea = default;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private bool attacking = false;
    private float timeToAttack = 0.25f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Gets the child of this object to create the attack area
        // the child should be the attack area polygon
        // attackArea = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // Activates the attack function when spacebar is pressed (needs to be modified for individual character use)
        if(Input.GetKeyDown(KeyCode.Space)) {
            Attack();
        }

        if(attacking) {
            timer += Time.deltaTime;

            if(timer >= timeToAttack) {
                timer = 0f;
                attacking = false;
                // attackArea.SetActive(attacking);
            }
        }
    }

    //attack function to time properly
    private void Attack() {
        attacking = true;
        // attackArea.SetActive(attacking);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
