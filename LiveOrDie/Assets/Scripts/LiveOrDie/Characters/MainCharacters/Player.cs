using System;
using UnityEngine;
public class Player : MonoBehaviour
{
    public int whichPlayer; // UNIQUE ID
    
    //stats fields
    public float speed = 3f; // speed of player movement
    public float maxRadius = 5f; // max distance between players
    public float characterHealth = 50f; // can be changed
    public float maxHealth = 50f;
    
    //some other possible stats
    //cooldown
    //pickRange
    //attackRange
    //attackDamage
    //attackSpeed
    //defence

    
    [HideInInspector]
    public bool isDead; // dead?
    private Player peer; // BEST FRIEND
    private Rigidbody2D rb; // Physics
    private DistanceJoint2D dj; // Physics
    private SpriteRenderer render; // PLAYER SKIN

    // REFERENCES
    [HideInInspector]
    public CharacterHealth healthbar;
    private CharacterMovement movement;
    // public functions
    public Rigidbody2D getRigidBody() {return rb;}

    public SpriteRenderer getSpriteRenderer() {return render;}
    public void EnforceSensitiveState(bool enforce){
        healthbar.setSensitiveState(enforce);
    }
    public void EnforceDrunkEffect(bool enforce) { 
        movement.ChangeDrunkState(enforce);
    }
    public void EnforceHealthEffect(string type) { 
        if(type == "drop"){
            healthbar.DecreaseHealth((int)characterHealth / 2);
        }
        else if(type == "boost"){
            healthbar.IncreaseHealth((int)maxHealth - (int)characterHealth);
        }
        else{
            Debug.Log("Wrong use of Event Listener for EnforceHealthEffect()");
        }
    }
    public void EnforceSpeedEffect(string type) { 
        if(type == "drop"){
            speed = 1;
        }
        else if(type == "boost"){
            speed *= 1.5f;
        }
        else if(type == "berzerkers"){
            speed *= 10f;
        }
        else{
            Debug.Log("Wrong use of Event Listener for EnforceSpeedEffect()");
        }
    }

    // setter functions
    private void KillPlayer() {isDead = true;}
    public void ResetCharacteristics(){
        speed = 5f;
        movement.ChangeDrunkState(false);
        healthbar.setSentitiveState(false);
        EventMgr.Instance.EventTrigger("MagicMushroom", false); // reset
    }
    protected void OnEnable(){
        isDead = false;
        if(!(rb = gameObject.GetComponent<Rigidbody2D>())) 
            rb = gameObject.AddComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        
        speed = 3f; // speed of player movement
        maxRadius = 5f; // max distance between players
        characterHealth = 50f; // can be changed
        maxHealth = 50f;
    }

    protected void Start() {
        switch (whichPlayer){
            case 1: // User 1 finds User 2 (right)
                peer = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>();
                dj = gameObject.AddComponent<DistanceJoint2D>();
                dj.connectedBody = peer.GetComponent<Rigidbody2D>();
                dj.distance = maxRadius;
                dj.maxDistanceOnly = true;
                break;
            case 2: // User 2 finds User 1 (left)
                peer = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>();
                dj = gameObject.AddComponent<DistanceJoint2D>();
                dj.connectedBody = peer.GetComponent<Rigidbody2D>();
                dj.maxDistanceOnly = true;
                break;
            default:
                Debug.LogWarning("Unexpected character type: " + whichPlayer);
                break;
        }
        healthbar = gameObject.GetComponentInChildren<CharacterHealth>();
        healthbar.playerPosition = gameObject.transform;
        if (!(movement = gameObject.GetComponent<CharacterMovement>()))
            movement = gameObject.AddComponent<CharacterMovement>();
        
        EventMgr.Instance.AddEventListener("PlayerDeath", KillPlayer);
        EventMgr.Instance.AddEventListener<E_LevelUpChoice>("LevelUp", LevelUp);
    }

    void Update()
    {
        if(isDead) {
            Destroy(gameObject);
        }
    }
    public void OnDestroy(){
        EventMgr.Instance.RemoveEventListener("PlayerDeath", KillPlayer);
        EventMgr.Instance.RemoveEventListener<E_LevelUpChoice>("LevelUp", LevelUp);
    }

    private void LevelUp(E_LevelUpChoice choice) //to do 
    {
        switch (choice)
        {
            case E_LevelUpChoice.IncreaseSpeed:
                print("speed");
                break;
            case E_LevelUpChoice.IncreaseMaxHealth:
                print("health");
                break;
            case E_LevelUpChoice.IncreaseRopeRadius:
                print("rope");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(choice), choice, null);
        }
    }

}
