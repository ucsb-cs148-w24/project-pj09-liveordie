using System;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    /* FOR LATER VARIABLES */
    // public bool isDead{get; private set;}
    // public int score;
    public float speed = 1.5f; // speed of player movement
    public float maxRadius = 5f; // max distance between players
    public int whichCharacter; // unique ID of character

    private BoxCollider2D boxCollide;
    private Rigidbody2D rb;
    
    // private Vector3 flip = new Vector3(3f, 3f, 1f);
    private GameObject peer;

    private DistanceJoint2D dj;

    public SpriteRenderer render;
    
    private bool isMovementLocked = false;  //movement lock flag

    public enum E_MoveType
    {
        up,
        down,
        left,
        right,
    }
    
    // void OnEnable(){
    // }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Enemy")){
            // Destroy(this.gameObject);
        }
    }
    void Start(){ 
        // Event listener  game pause events
        // EventMgr.Instance.AddEventListener("GamePaused", SwitchMovementLock);
        // EventMgr.Instance.AddEventListener("GameResumed", SwitchMovementLock);
        EventMgr.Instance.AddEventListener("GamePaused", GlobalControlLock);
        EventMgr.Instance.AddEventListener("GameResumed", GlobalControlUnlock);
        
        // Event listener  control events
        EventMgr.Instance.AddEventListener<E_AllKeysActs>("KeyIsHeld", Controls);

        //open key control lock (also create InputMgr instance)
        GlobalControlLock();
        
        render = this.GetComponent<SpriteRenderer>();
        rb = this.GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        boxCollide = this.AddComponent<BoxCollider2D>();
        boxCollide.isTrigger = true;


        switch (whichCharacter){ // identifies characters (you vs peer)
            case 1: // User 1 finds User 2 (right)
                peer = GameObject.FindGameObjectWithTag("Player2");
                dj = this.gameObject.AddComponent<DistanceJoint2D>();
                dj.connectedBody = peer.GetComponent<Rigidbody2D>();
                dj.distance = maxRadius;
                dj.maxDistanceOnly = true;
                break;
            case 2: // User 2 finds User 1 (left)
                peer = GameObject.FindGameObjectWithTag("Player1");
                break;
            default:
                Debug.LogWarning("Unexpected character type: " + whichCharacter);
                break;
        }
    }

    void Update()
    {

    }


    private void OnDestroy()
    {
        //remove all event listener
        // EventMgr.Instance.RemoveEventListener("GamePaused", SwitchMovementLock);
        // EventMgr.Instance.RemoveEventListener("GameResumed", SwitchMovementLock);
        EventMgr.Instance.RemoveEventListener("GamePaused", GlobalControlLock);
        EventMgr.Instance.RemoveEventListener("GameResumed", GlobalControlUnlock);
        EventMgr.Instance.RemoveEventListener<E_AllKeysActs>("KeyIsHeld", Controls);
    }
    
    //move character according to desired move type
    void MoveCharacter(E_MoveType moveType)
    {
        switch (moveType)
        {
            case E_MoveType.up:
                rb.position = new Vector2(rb.position.x, rb.position.y + Time.deltaTime * speed);
                break;
            case E_MoveType.down:
                rb.position = new Vector2(rb.position.x, rb.position.y  - Time.deltaTime * speed);
                break;
            case E_MoveType.left:
                rb.position = new Vector2(rb.position.x - Time.deltaTime * speed, rb.position.y);
                render.flipX = false;
                break;
            case E_MoveType.right:
                rb.position = new Vector2(rb.position.x + Time.deltaTime * speed, rb.position.y);
                render.flipX = true;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(moveType), moveType, null);
        }
        
    }

    private void Controls(E_AllKeysActs act) //called in update function in InputMgr
    {
        if (whichCharacter == 1)
        {
            switch (act)
            {
                case E_AllKeysActs.player1up:
                    MoveCharacter(E_MoveType.up);
                    break;
                case E_AllKeysActs.player1down:
                    MoveCharacter(E_MoveType.down);
                    break;
                case E_AllKeysActs.player1left:
                    MoveCharacter(E_MoveType.left);
                    break;
                case E_AllKeysActs.player1right:
                    MoveCharacter(E_MoveType.right);
                    break;
            }
        }
        else if (whichCharacter == 2)
        {
            switch (act)
            {
                case E_AllKeysActs.player2up:
                    MoveCharacter(E_MoveType.up);
                    break;
                case E_AllKeysActs.player2down:
                    MoveCharacter(E_MoveType.down);
                    break;
                case E_AllKeysActs.player2left:
                    MoveCharacter(E_MoveType.left);
                    break;
                case E_AllKeysActs.player2right:
                    MoveCharacter(E_MoveType.right);
                    break;
            }
        }
        
    }
    
    
    

    private void SwitchMovementLock()
    {
        isMovementLocked = !isMovementLocked;
    }

    private void GlobalControlLock()
    {
        InputMgr.Instance.SwitchAllButtons(false);
    }
    private void GlobalControlUnlock()
    {
        InputMgr.Instance.SwitchAllButtons(true);
    }
    
    
}
