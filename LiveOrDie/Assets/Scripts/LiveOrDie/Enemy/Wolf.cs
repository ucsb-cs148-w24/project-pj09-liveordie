using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Enemy
{
    private SpriteRenderer render;
    private Rigidbody2D rb;
    private EnemyHealth enemyHealth;

    public override void Initialize() {
        health = 10;
        damage = 1;
        isDead = false;
        SetTarget();
        enemyHealth = GetComponentInChildren<EnemyHealth>();
    }

    void OnEnable()
    {
        Initialize();
    }

    void OnDisable()
    {
        if(gameObject){
            Destroy(gameObject);
        }
    }

    private void Update() {
        if(isDead && gameObject) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (!isDead && other.CompareTag("Bullet")){
            enemyHealth.DecreaseHealth();
        }
    }

    private void SetTarget() {
        render = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        int chooseTarget = UnityEngine.Random.Range(0, 2);
        if (chooseTarget == 0) target = GameObject.FindGameObjectWithTag("Player1");
        else target = GameObject.FindGameObjectWithTag("Player2");
        if(chooseTarget == 0) render.color = Color.white;
        else render.color = Color.grey;
    }
}
