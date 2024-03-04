using UnityEngine;
using UnityEngine.TextCore.Text;
public class Player : MonoBehaviour
{
    public int whichPlayer; // UNIQUE ID
    
    //stats fields
    public float speed = 1.5f; // speed of player movement
    public float maxRadius = 5f; // max distance between players
    public float characterHealth = 50f; // can be changed
    public float maxHealth = 50f;
    
    //some other possible stats
    //cooldown
    //pickRange
    //attackRange
    //attackDamage
    //attackSpeed

    
    [HideInInspector]
    public bool isDead; // dead?
    private Player peer; // BEST FRIEND
    private Rigidbody2D rb; // Physics
    private DistanceJoint2D dj; // Physics
    private SpriteRenderer render; // PLAYER SKIN

    // REFERENCES
    private CharacterHealth healthBar;
    private CharacterMovement movement;

    // getter functions
    public Rigidbody2D getRigidBody() {return rb;}

    // setter functions
    private void KillPlayer() {isDead = true;}

    protected void OnEnable(){
        isDead = false;
        if(!(rb = gameObject.GetComponent<Rigidbody2D>())) 
            rb = gameObject.AddComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }
    protected void Start() {
        EventMgr.Instance.AddEventListener("PlayerDeath", KillPlayer);
        EventMgr.Instance.AddEventListener("LevelUp", LevelUp);
    }

    void Update()
    {
        if(isDead) {
            Destroy(gameObject);
        }
    }
    public void OnDestroy(){
        EventMgr.Instance.RemoveEventListener("PlayerDeath", KillPlayer);
    }

    private void LevelUp() //to do 
    {
        
    }

}
