using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dragon : Enemy
{
    private Rigidbody2D rb;
    private EnemyHealth enemyHealth;
    private int points = 500; // how many points a dragon is worth
    private Vector3 moveDir;

    private Transform tailTransform;
    private Vector3 tailPosition;

    private Transform headTransform;
    private Vector3 headPosition;
    public override void Initialize() {
        health = 100;
        damage = 500;
        SetTarget();

        render = GetComponentsInChildren<SpriteRenderer>().ToList().Where(p => p.name=="head").FirstOrDefault();
        tailTransform =  GetComponentsInChildren<Transform>().ToList().Where(p => p.name=="tail end").FirstOrDefault();
        headTransform =  GetComponentsInChildren<Transform>().ToList().Where(p => p.name=="head").FirstOrDefault();
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

    private void Update()
    {
        tailPosition = Camera.main.WorldToViewportPoint(tailTransform.position);
        headPosition = Camera.main.WorldToViewportPoint(headTransform.position);
        if ((tailPosition.x < 0 || tailPosition.x > 1f || tailPosition.y < 0 || tailPosition.y > 1f) &&
            (headPosition.x < 0 || headPosition.x > 1f || headPosition.y < 0 || headPosition.y > 1f)) 
        {
            SetTarget();
        }

    }

    public override void TakeDamage(int damage) {
        base.TakeDamage(damage);
        enemyHealth.UpdateHealthBar();
        CheckDead();
    }

    private void SetTarget() {
        int chooseTarget = Random.Range(0, 2);
        if (chooseTarget == 0) target = GameObject.FindGameObjectWithTag("Player1");
        else target = GameObject.FindGameObjectWithTag("Player2");
        this.transform.right = -(target.transform.position - this.transform.position);
        
    }

    // public void Attack()
    // {
    //     if(!this) return;
    //     PoolMgr.Instance.GetObjAsync("Prefabs/dragon_fire", (vomit) => {
    //         if(!vomit) return;
    //         vomit.transform.position = (gameObject.transform.position);
    //         vomit.transform.parent = transform;
    //         DragonVomitAttackBehavior dragonVomitAttack = GetComponent<DragonVomitAttackBehavior>();
    //         dragonVomitAttack.Initialize();
    //         // dragonVomitAttack.StartAutoAttack();
    //     });
    // }
    private void CheckDead()
    {
        if (health <= 0){
            Die();
        }
    }
    

    protected override void Die()
    {
        EventMgr.Instance.EventTrigger("DragonDead"); //trigger event for later usage
        EventMgr.Instance.EventTrigger("IncrementScore", points);
        EventMgr.Instance.EventTrigger("DropExp", this.gameObject.transform.position);
        PoolMgr.Instance.PushObj("Prefabs/Dragon",this.gameObject); //push gameObject back to pool
    }
}
