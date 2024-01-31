using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {

    }

    private void onTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag == "Enemy") {
            Destroy(col.gameObject);
        }
    }
}
