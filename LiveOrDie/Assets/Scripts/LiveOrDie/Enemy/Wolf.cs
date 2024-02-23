using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Enemy
{
    private SpriteRenderer render;
    private Rigidbody2D rb;
    private EnemyHealth enemyHealth;

    private CharacterMovement mostRecentAttacker; // keeps track of the last person who attacked them

    public override void Initialize() {
        health = 10;
        damage = 1;
        SetTarget();
        
        //subcomponents
        enemyHealth = GetComponentInChildren<EnemyHealth>();
        enemyHealth.enemy = this; //assign itself to its sub component
        enemyHealth.Initialize();
    }

    void OnEnable()
    {
        Initialize();
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Bullet"))
        {
            health--;
            mostRecentAttacker = other.GetComponentInParent<CharacterMovement>();
            enemyHealth.UpdateHealthBar();
            CheckDead(); //should be move out of the if/switch case when there are more ways to reduce health
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

    private void CheckDead()
    {
        if (health <= 0){
            EventMgr.Instance.EventTrigger("IncrementScore", mostRecentAttacker.whichCharacter);
            Die();
        }
    }
    

    protected override void Die()
    {
        EventMgr.Instance.EventTrigger("WolfDead"); //trigger event for later usage
        PoolMgr.Instance.PushObj("Prefabs/Wolf",this.gameObject); //push gameObject back to pool
    }
}
