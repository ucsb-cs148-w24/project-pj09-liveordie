using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMovement : MonoBehaviour
{
    private float speed = 10f; // move speed of enemies
    private GameObject target;
    private int chooseTarget;
    // private SpriteRenderer render;
    private Rigidbody2D rb;
    private CircleCollider2D collide;

    // Start is called before the first frame update
    void OnEnable()
    {
        // render = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        collide = GetComponent<CircleCollider2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        chooseTarget = Random.Range(0, 2);
        if (chooseTarget == 0) target = GameObject.FindGameObjectWithTag("Player1");
        else target = GameObject.FindGameObjectWithTag("Player2");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {
        // TrackDistance();
        if (target == null) return;
        this.transform.position += -this.transform.right * speed * Time.deltaTime;
    }
}
