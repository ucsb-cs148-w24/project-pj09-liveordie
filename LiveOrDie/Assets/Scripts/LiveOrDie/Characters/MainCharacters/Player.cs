using UnityEngine;
public class Player : MonoBehaviour
{
    public int whichPlayer; // UNIQUE ID

    [HideInInspector]
    public float maxRadius {get; set;} // max distance between players
    [HideInInspector]
    public bool isDead; // dead?
    private Player peer; // BEST FRIEND
    private Rigidbody2D rb; // Physics
    private DistanceJoint2D dj; // Physics
    private SpriteRenderer render; // PLAYER SKIN

    // REFERENCES
    [HideInInspector]
    private CharacterHealth healthbar;
    [HideInInspector]
    private CharacterMovement movement;

    // public functions
    public Rigidbody2D getRigidBody() {return rb;}
    public SpriteRenderer getSpriteRenderer() {return render;}
    public void DropHealthEffect() { healthbar.DecreaseHealth((int)healthbar.characterHealth / 2);}
    public void BoostHealthEffect() { healthbar.IncreaseHealth((int)healthbar.maxHealth - (int)healthbar.characterHealth);}
    public void BoostSpeedEffect() { movement.speed *= 1.5f;}
    public void SlugSpeedEffect() { movement.speed = 1;}
    public void UpdateTempEffectBoolean(){underTempEffect = true;}

    // setter functions
    private void KillPlayer() {isDead = true;}
    private void ResetCharacteristics(){
        movement.speed = 5f;
    }
    private float timerEffect = 5f;
    private bool underTempEffect;

    protected void OnEnable(){
        maxRadius = 5f;
        isDead = false;
        underTempEffect = false;
        if(!(render = gameObject.GetComponent<SpriteRenderer>())) 
            render = gameObject.AddComponent<SpriteRenderer>();
        if(!(rb = gameObject.GetComponent<Rigidbody2D>())) 
            rb = gameObject.AddComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        //WeaponState = default weapons
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
    }

    void Update()
    {
        if(isDead) {
            Destroy(gameObject);
        }
        else{
            if(underTempEffect){
                timerEffect -= Time.deltaTime;
                if(timerEffect <= 0){
                    underTempEffect = false;
                    ResetCharacteristics();
                }
            }
        }
    }
    public void OnDestroy(){
        EventMgr.Instance.RemoveEventListener("PlayerDeath", KillPlayer);
        healthbar.SelfDestruct();
        // weapon.SelfDestruct();
    }
    public void OnDisable(){
        if(gameObject) Destroy(gameObject);
    }

}
