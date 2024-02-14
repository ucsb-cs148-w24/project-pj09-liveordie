using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private GameObject target;
    private SpriteRenderer render;
    private Rigidbody2D rb;
    private CircleCollider2D collide;
    private EnemyHealth healthbar;
    private NavMeshAgent agent;
    public GameObject prefab;
    private bool isDead = false;
    void Start()
    {
        healthbar = GetComponentInChildren<EnemyHealth>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
		agent.updateUpAxis = false;
    }
    public void Kill(){
        isDead = true;
    }
    void OnEnable()
    {
        render = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        collide = GetComponent<CircleCollider2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        int chooseTarget = UnityEngine.Random.Range(0, 2);
        if (chooseTarget == 0) target = GameObject.FindGameObjectWithTag("Player1");
        else target = GameObject.FindGameObjectWithTag("Player2");
        if(chooseTarget == 0) render.color = Color.white;
        else render.color = Color.grey;

        Vector3 pos = transform.localPosition;
        GameObject instance = Instantiate(prefab, new Vector3(pos.x, pos.y+1.2f, pos.z), Quaternion.identity);
        instance.transform.SetParent(gameObject.transform);
    }

    void OnDisable()
    {
        if(gameObject){
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead && target) Move();
        else {
            Destroy(healthbar);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
        if (!isDead && other.CompareTag("Bullet")){
            healthbar.DecreaseHealth();
        }
    }
    private void Move()
    {
        agent.SetDestination(target.transform.position);
        // Vector3 direction = (target.transform.position - transform.position).normalized;
        // this.transform.position += direction * speed * Time.deltaTime;

        if(agent.desiredVelocity.x > 0) render.flipX = true;
        else render.flipX = false;
    }
}
