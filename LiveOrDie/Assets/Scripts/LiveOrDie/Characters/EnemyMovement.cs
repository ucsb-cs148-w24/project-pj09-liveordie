using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private SpriteRenderer render;
    private NavMeshAgent agent;
    private Enemy enemy;

    void Start()
    {
        render = GetComponentInChildren<SpriteRenderer>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
		agent.updateUpAxis = false;
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!enemy.isDead && enemy.target) Move();
    }
    
    private void Move()
    {
        agent.SetDestination(enemy.target.transform.position);
        if(agent.desiredVelocity.x > 0) render.flipX = true;
        else render.flipX = false;
    }
}
