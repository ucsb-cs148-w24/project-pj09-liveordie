using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 0.5f; // move speed of enemies
    private GameObject target;
    private int chooseTarget;
    private SpriteRenderer render;
    private Rigidbody2D rb;
    private CircleCollider2D collide;
    void Start()
    {

    }

    void OnEnable()
    {
        render = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        collide = GetComponent<CircleCollider2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        chooseTarget = Random.Range(0, 2);
        if (chooseTarget == 0) target = GameObject.FindGameObjectWithTag("Player1");
        else target = GameObject.FindGameObjectWithTag("Player2");
        if(chooseTarget == 0) render.color = Color.red;
        else render.color = Color.blue;
    }

    void OnDisable()
    {
        if(gameObject != null) Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Bullet")){
            Debug.Log("Damage done to Enemy!");
            Destroy(this.gameObject);
        }
        else if(other.CompareTag(target.tag)){
            Debug.Log("Damage done to Character");
            Destroy(this.gameObject);
        }
    }
    private void Move()
    {
        // TrackDistance();
        Vector3 direction = (target.transform.position - transform.position).normalized;
        this.transform.position += direction * speed * Time.deltaTime;
    }

    // private void TrackDistance()
    // {
    //     float dist = Vector3.Distance(target.transform.position, transform.position);
        // if(dist <= 0.1){
        //     OnDisable();
        // }
    // }
}
