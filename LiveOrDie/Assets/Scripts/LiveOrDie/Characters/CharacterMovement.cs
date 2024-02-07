using System.Collections;
using System.Collections.Generic;
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

    private BoxCollider2D collide;
    private Rigidbody2D rb;
    private Vector3 flip = new Vector3(3f, 3f, 1f);
    private GameObject peer;

    private DistanceJoint2D dj;

    // void OnEnable(){
    // }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Enemy")){
            // Destroy(this.gameObject);
        }
    }
    void Start(){ 
        rb = this.GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        collide = this.AddComponent<BoxCollider2D>();
        
        collide.isTrigger = true;
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
        Vector3 pos = this.transform.position;
 
        if(whichCharacter == 2 || whichCharacter == 1){
            MoveCharacter(ref pos, whichCharacter);
        } else{ Debug.LogWarning("Unexpected character type: " + whichCharacter); }

        rb.MovePosition(pos);
    }

    // Controls response to keyboard movement
    void MoveCharacter(ref Vector3 pos, int playerID){
        if((playerID == 1 && Input.GetKey("a")) 
        || (playerID == 2 && Input.GetKey(KeyCode.LeftArrow))){
            pos.x -= Time.deltaTime * speed;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if((playerID == 1 && Input.GetKey("d"))
        || (playerID == 2 && Input.GetKey(KeyCode.RightArrow))){
            pos.x += Time.deltaTime * speed;
            GetComponent<SpriteRenderer>().flipX = true;
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
}
