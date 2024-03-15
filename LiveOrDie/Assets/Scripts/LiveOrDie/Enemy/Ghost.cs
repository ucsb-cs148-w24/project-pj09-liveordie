using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    private Rigidbody2D rb;
    private EnemyHealth enemyHealth;
    private int points = 20; // how many points a ghost is worth

    private CharacterMovement mostRecentAttacker; // keeps track of the last person who attacked them

    [HideInInspector]
    public string deathEvent = "EnemyDead";
    public string dropEvent = "DropExp";

    public override void Initialize() {
        health = 5;
        maxHealth = 5;
        damage = 10;
        SetTarget();

        EnemyAttackAudioName="ghostAttack";

        render = GetComponent<SpriteRenderer>();
        render.material = originalMat;

        
        //subcomponents
        enemyHealth = GetComponentInChildren<EnemyHealth>();
        enemyHealth.enemy = this; //assign itself to its sub component
        enemyHealth.Initialize();
        
        this.gameObject.transform.localScale = Vector3.one; //reset if elite
        deathEvent = "EnemyDead";
        dropEvent = "DropExp";
    }

    void OnEnable()
    {
        Initialize();
    }

    public override void TakeDamage(int damage) {
        AudioMgr.Instance.PlayAudio("ghostHurt",false);
        base.TakeDamage(damage);
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
        // if(chooseTarget == 0) render.color = Color.white;
        // else render.color = Color.grey;
    }

    private void CheckDead()
    {
        if (health <= 0){
            Die();
        }
    }
    

    protected override void Die()
    {
        EventMgr.Instance.EventTrigger(deathEvent); //trigger event for later usage
        EventMgr.Instance.EventTrigger("IncrementScore", points);
        EventMgr.Instance.EventTrigger(dropEvent, this.gameObject.transform.position);
        PoolMgr.Instance.PushObj("Prefabs/Ghost",this.gameObject); //push gameObject back to pool
    }
}
