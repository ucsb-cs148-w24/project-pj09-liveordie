using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMovement : MonoBehaviour
{
    private float speed = 6f; // move speed of enemies
    private GameObject target;
    private int chooseTarget;
    // private SpriteRenderer render;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void OnEnable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {
        this.transform.position += -this.transform.right * speed * Time.deltaTime;
    }
}
