using Unity.VisualScripting;
using UnityEngine;

public class PewPew : MonoBehaviour
{
    // A BULLET
    public float bulletSpeed = 10f;
    public Rigidbody2D bulletRb;
    private Vector3 lowerBound;
    private Vector3 upperBound;
    private CapsuleCollider2D collide;
    private SpriteRenderer childRender, parentRender;
    void OnEnable(){
        childRender = this.GetComponent<SpriteRenderer>();
        childRender.color = Color.yellow;
        bulletRb = this.AddComponent<Rigidbody2D>();
        collide = this.AddComponent<CapsuleCollider2D>();   
        bulletRb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        bulletRb.mass = 1;
        bulletRb.gravityScale = 0;
        collide.isTrigger = true;
        lowerBound = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0)); 
        upperBound = Camera.main.ViewportToWorldPoint(new Vector3(1,1,0));

    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Enemy")){
            Destroy(gameObject);
        }
    }
    void Start()
    {
        parentRender = this.GetComponentInParent<CharacterMovement>().render;
        if(parentRender.flipX == false){
            bulletRb.velocity = -transform.right * bulletSpeed;
        }
        else if (parentRender.flipX == true){
            bulletRb.velocity = transform.right * bulletSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckBounds();
    }
    void CheckBounds(){
        Vector3 curr_position = this.gameObject.transform.position;
        if (curr_position.x > upperBound.x || curr_position.x < lowerBound.x
            || curr_position.y > upperBound.y || curr_position.y < lowerBound.y){
                Destroy(gameObject);
            }

    }
    void OnDisable(){
        if(gameObject != null) Destroy(gameObject);
    }
}
