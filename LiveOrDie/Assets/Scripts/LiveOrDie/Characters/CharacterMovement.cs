using System;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 1.5f; // speed of player movement
    public float maxRadius = 5f; // max distance between players
    public int whichCharacter; // unique ID of character

    private BoxCollider2D boxCollide;
    private Rigidbody2D rb;
    private GameObject peer;
    private DistanceJoint2D dj;
    public SpriteRenderer render;
    private CharacterHealth healthbar;
    private bool isDead = false;
    
    //movement lock flag
    public bool isMovementLocked = false;

    public void Kill(){
        isDead = true;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(!isDead && other.CompareTag("Enemy")){
            // FOR NOW, bumping into character will "hurt" player, 
            // later, ideal if there are more means of attack
            healthbar.DecreaseHealth();
        }
    }
    void Start(){ 
        healthbar = GetComponentInChildren<CharacterHealth>();
        // Event listener
        EventMgr.Instance.AddEventListener("GamePaused", SwitchMovementLock);
        EventMgr.Instance.AddEventListener("GameResumed", SwitchMovementLock);
        
        
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
        if(isDead) {
            Destroy(healthbar);
            Destroy(gameObject);
        }
        else{
            Vector3 pos = this.transform.position;
            if (!isMovementLocked) // stop if movement locked
            {
                
                if (whichCharacter == 2 || whichCharacter == 1)
                {
                    MoveCharacter(ref pos, whichCharacter);
                }
                else
                {
                    Debug.LogWarning("Unexpected character type: " + whichCharacter);
                }
                rb.MovePosition(pos);
            }
        }
    }
    void OnDisable(){
        if(gameObject){
            Destroy(gameObject);
        }
    }


    private void OnDestroy()
    {
        EventMgr.Instance.RemoveEventListener("GamePaused", SwitchMovementLock);
        EventMgr.Instance.RemoveEventListener("GameResumed", SwitchMovementLock);
    }

    // Controls response to keyboard movement
    void MoveCharacter(ref Vector3 pos, int playerID){
        if((playerID == 1 && Input.GetKey("a")) 
        || (playerID == 2 && Input.GetKey(KeyCode.LeftArrow))){
            pos.x -= Time.deltaTime * speed;
            render.flipX = false;
        }
        if((playerID == 1 && Input.GetKey("d"))
        || (playerID == 2 && Input.GetKey(KeyCode.RightArrow))){
            pos.x += Time.deltaTime * speed;
            render.flipX = true;
        }
        if((playerID == 1 && Input.GetKey("s"))
        || (playerID == 2 && Input.GetKey(KeyCode.DownArrow))){
            pos.y -= Time.deltaTime * speed;
        }
        if((playerID == 1 && Input.GetKey("w"))
        || (playerID == 2 && Input.GetKey(KeyCode.UpArrow))){
            pos.y += Time.deltaTime * speed;
        }
    }

    private void SwitchMovementLock()
    {
        isMovementLocked = !isMovementLocked;
    }
    
    
}
