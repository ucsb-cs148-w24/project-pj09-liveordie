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

    private Vector3 flip = new Vector3(3f, 3f, 1f);
    GameObject peer;
    private Rigidbody2D rb;
    void Start()
    {
        switch (whichCharacter){ // identifies characters (you vs peer)
            case 1: // User 1 finds User 2 (right)
                peer = GameObject.FindGameObjectWithTag("Player2");
                break;
            case 2: // User 2 finds User 1 (left)
                peer = GameObject.FindGameObjectWithTag("Player1");
                break;
            default:
                Debug.LogWarning("Unexpected character type: " + whichCharacter);
                break;
        }
        rb = GetComponent<Rigidbody2D>();
        // this.transform.localPosition = new Vector3(0, 0, 0); // sets default position
    }

    void Update()
    {
        Vector3 pos = this.transform.position;
        // float distance = Vector3.Distance(pos, peer.transform.position);
        
        // if(distance > maxRadius){ // maxRadius reached
        //     Debug.Log("SOUL BOND IS DYING!! Current Distance: " + distance); // for debugging purposes
        //     Vector3 direction = (peer.transform.position - transform.position).normalized;
        //     pos += direction * speed;
        // }
        // else{
            if(whichCharacter == 2 || whichCharacter == 1){
                MoveCharacter(ref pos, whichCharacter);
            } else{ Debug.LogWarning("Unexpected character type: " + whichCharacter); }
        // }
        rb.MovePosition(pos);
        // this.transform.position = pos;
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
