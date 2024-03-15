using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using UnityEngine;

public class Dragon : Enemy
{
    private Rigidbody2D rb;
    private EnemyHealth enemyHealth;
    private int chooseTarget;
    private int points = 500; // how many points a dragon is worth
    public override void Initialize() {
        health = 100;
        damage = 500;
        SetTarget();

        render = GetComponentsInChildren<SpriteRenderer>().ToList().Where(p => p.name=="head").FirstOrDefault();
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
        enemyHealth.UpdateHealthBar();
        CheckDead();
    }

    private void SetTarget() {
        // render = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        int chooseTarget = UnityEngine.Random.Range(0, 2);
        if (chooseTarget == 0) target = GameObject.FindGameObjectWithTag("Player1");
        else target = GameObject.FindGameObjectWithTag("Player2");
        // if(chooseTarget == 0) render.color = Color.white;
        // else render.color = Color.grey;
    }

    public void Attack()
    {
        if(!this) return;
        PoolMgr.Instance.GetObjAsync("Prefabs/dragon_fire", (vomit) => {
            if(!vomit) return;
            vomit.transform.position = (gameObject.transform.position);
            vomit.transform.parent = transform;
            DragonVomitAttackBehavior dragonVomitAttack = GetComponent<DragonVomitAttackBehavior>();
            dragonVomitAttack.Initialize();
            // dragonVomitAttack.StartAutoAttack();
        });
    }
    private void CheckDead()
    {
        if (health <= 0){
            Die();
        }
    }
    

    protected override void Die()
    {
        // EventMgr.Instance.EventTrigger("DragonDead"); //trigger event for later usage
        EventMgr.Instance.EventTrigger("IncrementScore", points);
        EventMgr.Instance.EventTrigger("DropExp", this.gameObject.transform.position);
        PoolMgr.Instance.PushObj("Prefabs/Dragon",this.gameObject); //push gameObject back to pool
    }
}
