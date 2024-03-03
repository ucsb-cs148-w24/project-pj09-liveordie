using System;
using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class CharacterMovement : MonoBehaviour
{
    public float speed = 1.5f; // speed of player movement
    public float maxRadius = 5f; // max distance between players
    public int whichCharacter; // unique ID of character
    private BoxCollider2D boxCollide;
    private Rigidbody2D rb;
    private GameObject peer;
    private DistanceJoint2D dj;
    private SpriteRenderer render;
    private CharacterHealth healthbar;
    private bool isDead = false;
    
    //movement lock flag
    public bool isMovementLocked = false;
    public int horizontal;
    public int vertical;

    public enum E_MoveType
    {
        up,
        down,
        left,
        right,
    }
    public void SelfDestruct() { Destroy(gameObject); }
    public void OnEnable(){
        speed = 10f;
        horizontal = vertical = 0;
    }
    void Start(){ 
        healthbar = GetComponentInChildren<CharacterHealth>();

        // Event listener
        EventMgr.Instance.AddEventListener("GamePaused", GlobalControlLock);
        EventMgr.Instance.AddEventListener("GameResumed", GlobalControlUnlock);
        
        // Event listener  control events
        EventMgr.Instance.AddEventListener<E_AllKeysActs>("KeyIsHeld", Controls);
        EventMgr.Instance.AddEventListener<E_AllKeysActs>("KeyIsReleased", CancelControls);

        //open key control lock (also create InputMgr instance)
        GlobalControlUnlock();
        
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
    }
    void OnDisable(){
        if(gameObject){ Destroy(gameObject); }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + Time.deltaTime * speed * new Vector2(horizontal, vertical).normalized);
    }

    private void OnDestroy()
    {
        //remove all event listener
        // EventMgr.Instance.RemoveEventListener("GamePaused", SwitchMovementLock);
        // EventMgr.Instance.RemoveEventListener("GameResumed", SwitchMovementLock);
        EventMgr.Instance.RemoveEventListener("GamePaused", GlobalControlLock);
        EventMgr.Instance.RemoveEventListener("GameResumed", GlobalControlUnlock);
        EventMgr.Instance.RemoveEventListener<E_AllKeysActs>("KeyIsHeld", Controls);
        EventMgr.Instance.AddEventListener<E_AllKeysActs>("KeyIsReleased", CancelControls);
    }
    
    //move character according to desired move type
    void ChangeCharacterDirection(E_MoveType moveType)
    {
        switch (moveType)
        {
            case E_MoveType.up:
                vertical = 1;
                break;
            case E_MoveType.down:
                vertical = -1;
                break;
            case E_MoveType.left:
                horizontal = -1;
                render.flipX = false;
                break;
            case E_MoveType.right:
                horizontal = 1;
                render.flipX = true;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(moveType), moveType, null);
        }
    }
    
    //set the move direction (Hold key action)
    private void Controls(E_AllKeysActs act) //called in Update function in InputMgr
    {
        if (whichCharacter == 1)
        {
            switch (act)
            {
                case E_AllKeysActs.player1up:
                    ChangeCharacterDirection(E_MoveType.up);
                    break;
                case E_AllKeysActs.player1down:
                    ChangeCharacterDirection(E_MoveType.down);
                    break;
                case E_AllKeysActs.player1left:
                    ChangeCharacterDirection(E_MoveType.left);
                    break;
                case E_AllKeysActs.player1right:
                    ChangeCharacterDirection(E_MoveType.right);
                    break;
            }
        }
        else if (whichCharacter == 2)
        {
            switch (act)
            {
                case E_AllKeysActs.player2up:
                    ChangeCharacterDirection(E_MoveType.up);
                    break;
                case E_AllKeysActs.player2down:
                    ChangeCharacterDirection(E_MoveType.down);
                    break;
                case E_AllKeysActs.player2left:
                    ChangeCharacterDirection(E_MoveType.left);
                    break;
                case E_AllKeysActs.player2right:
                    ChangeCharacterDirection(E_MoveType.right);
                    break;
            }
        }
    }

    //clear the move direction (Release key action)
    private void CancelControls(E_AllKeysActs act) //called in Update function in InputMgr
    {
        if (whichCharacter == 1)
        {
            switch (act)
            {
                case E_AllKeysActs.player1up:
                case E_AllKeysActs.player1down:
                    vertical = 0;
                    break;
                case E_AllKeysActs.player1left:
                case E_AllKeysActs.player1right:
                    horizontal = 0;
                    break;
            }
        }
        else if (whichCharacter == 2)
        {
            switch (act)
            {
                case E_AllKeysActs.player2up:
                case E_AllKeysActs.player2down:
                    vertical = 0;
                    break;
                case E_AllKeysActs.player2left:
                case E_AllKeysActs.player2right:
                    horizontal = 0;
                    break;
            }
        }
        
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
