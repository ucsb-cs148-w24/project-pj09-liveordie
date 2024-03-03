using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public float speed = 2f; // move speed of enemies
    private GameObject target;
    private int chooseTarget;
    private SpriteRenderer render;
    private Rigidbody2D rb;
    private CircleCollider2D collide;

    // Start is called before the first frame update
    void OnEnable()
    {
        render = GetComponent<SpriteRenderer>();
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
        Vector3 direction = (target.transform.position - transform.position).normalized;
        this.transform.position += direction * speed * Time.deltaTime;

        // Debug.Log(this.transform.position.x);
        // Debug.Log(direction.x);

        if(direction.x > 0) render.flipX = true;
        else render.flipX = false;
    }
}
