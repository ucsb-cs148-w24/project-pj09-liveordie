using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Enemy
{
    private Rigidbody2D rb;
    private EnemyHealth enemyHealth;
    private int points = 10; // how many points a wolf is worth

    private CharacterMovement mostRecentAttacker; // keeps track of the last person who attacked them

    public override void Initialize() {
        health = 10;
        damage = 1;
        SetTarget();
        render = GetComponent<SpriteRenderer>();
        render.material = originalMat;
        
        //subcomponents
        enemyHealth = GetComponentInChildren<EnemyHealth>();
        enemyHealth.enemy = this; //assign itself to its sub component
        enemyHealth.Initialize();
    }

    void OnEnable()
    {
        Initialize();
    }

    public override void TakeDamage(int damage) {
        base.TakeDamage(damage);
        AudioMgr.Instance.PlayAudio("wolfHurt",false);
        enemyHealth.UpdateHealthBar();
        CheckDead();
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
            AudioMgr.Instance.PlayAudio("wolfDie",false);
            Die();
        }
    }
    

    protected override void Die()
    {
        EventMgr.Instance.EventTrigger("EnemyDead"); //trigger event for later usage
        EventMgr.Instance.EventTrigger("IncrementScore", points);
        EventMgr.Instance.EventTrigger("DropExp", this.gameObject.transform.position);
        PoolMgr.Instance.PushObj("Prefabs/Wolf",this.gameObject); //push gameObject back to pool
    }
}
