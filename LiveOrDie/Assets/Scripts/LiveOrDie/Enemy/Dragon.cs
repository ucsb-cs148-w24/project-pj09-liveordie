using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dragon : Enemy
{
    private EnemyHealth enemyHealth;
    private int points = 500; // how many points a dragon is worth
    private Vector3 moveDir;

    private Transform tailTransform;
    private Vector3 tailPosition;

    private Transform headTransform;
    private Vector3 headPosition;

    private float attackCoolDownTime = 0.2f;

    private int chooseTarget;
    
    
    private Player player1, player2;
    private Player currentTarget;
    private bool canAttack = true;
    public override void Initialize() {
        health = 150;
        maxHealth = 150;
        damage = 2;
        SetTargetDirection();

        render = GetComponentsInChildren<SpriteRenderer>().ToList().FirstOrDefault(p => p.name=="head");
        tailTransform =  GetComponentsInChildren<Transform>().ToList().FirstOrDefault(p => p.name=="tail end");
        headTransform =  GetComponentsInChildren<Transform>().ToList().FirstOrDefault(p => p.name=="head");
        render.material = originalMat;

        //subcomponents
        enemyHealth = GetComponentInChildren<EnemyHealth>();
        enemyHealth.enemy = this; //assign itself to its sub component
        enemyHealth.Initialize();
    }

    void OnEnable()
    {
        Initialize();
        player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>();
        player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>();
        canAttack = true;
    }

    private void Update()
    {
        tailPosition = Camera.main.WorldToViewportPoint(tailTransform.position);
        headPosition = Camera.main.WorldToViewportPoint(headTransform.position);
        if ((tailPosition.x < 0 || tailPosition.x > 1f || tailPosition.y < 0 || tailPosition.y > 1f) &&
            (headPosition.x < 0 || headPosition.x > 1f || headPosition.y < 0 || headPosition.y > 1f)) 
        {
            chooseTarget = Random.Range(0, 2);
            SetTargetDirection();
        }

    }

    public override void TakeDamage(int damage) {
        base.TakeDamage(damage);
        enemyHealth.UpdateHealthBar();
        CheckDead();
    }

    private void SetTargetDirection() {
        if (chooseTarget == 0) target = GameObject.FindGameObjectWithTag("Player1");
        else target = GameObject.FindGameObjectWithTag("Player2");
        this.transform.right = -(target.transform.position - this.transform.position);
        
    }
    
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
        EventMgr.Instance.EventTrigger("DropWeaponUnlock", this.gameObject.transform.position);
        PoolMgr.Instance.PushObj("Prefabs/Dragon",this.gameObject); //push gameObject back to pool
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!player1 || !player2 || player1.isDead || player2.isDead) return;
        if (other.gameObject.CompareTag("Player1"))
            currentTarget = player1;
        else if (other.gameObject.CompareTag("Player2"))
            currentTarget = player2;

        if (currentTarget != null && canAttack )
        {
            currentTarget.GetComponentInChildren<CharacterHealth>().DecreaseHealth(damage);
            
            canAttack = false;
            if(this.gameObject.activeSelf) StartCoroutine(AttackCoolDownCoroutine());
        }
        currentTarget = null;
    }
    
    
    IEnumerator AttackCoolDownCoroutine()
    {
        yield return new WaitForSeconds(attackCoolDownTime);
        canAttack = true;
    }

    void OnDisable()
    {
        StopCoroutine(AttackCoolDownCoroutine());
    }
}
