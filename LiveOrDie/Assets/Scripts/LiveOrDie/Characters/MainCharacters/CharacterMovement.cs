using System;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class CharacterMovement : MonoBehaviour
{
    public float maxRadius = 5f; // max distance between players
    
    //components
    private Player playerModel;
    public int whichCharacter; // unique ID of character
    private BoxCollider2D boxCollide;
    private Rigidbody2D rb;
    private GameObject peer;
    private DistanceJoint2D dj;
    private SpriteRenderer render;
    private CharacterHealth healthbar;
    private bool isDead = false;
    
    //movement lock flag
    public int horizontal;
    public int vertical;
    public bool drunkState=false;
    private List<int> drunkMovements;

    public enum E_MoveType
    {
        up,
        down,
        left,
        right,
    }
    public void SelfDestruct() { Destroy(gameObject); }
    public void OnEnable(){
        horizontal = vertical = 0;
        drunkMovements = new List<int>{0,1,2,3};
    }

    public void ChangeDrunkState(bool enforce){
        drunkState = enforce;
        drunkMovements = ShuffleList(drunkMovements);
    }
    void Start()
    {
        playerModel = GetComponent<Player>();

        // Event listener
        EventMgr.Instance.AddEventListener("GamePaused", GlobalControlLock);
        EventMgr.Instance.AddEventListener("GameResumed", GlobalControlUnlock);
        EventMgr.Instance.AddEventListener<bool>("DrunkDrug", ChangeDrunkState);
        
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
                dj.autoConfigureDistance = false;
                break;
            case 2: // User 2 finds User 1 (left)
                peer = GameObject.FindGameObjectWithTag("Player1");
                dj = gameObject.AddComponent<DistanceJoint2D>();
                dj.connectedBody = peer.GetComponent<Rigidbody2D>();
                dj.maxDistanceOnly = true;
                dj.autoConfigureDistance = false;
                break;
            default:
                Debug.LogWarning("Unexpected character type: " + whichCharacter);
                break;
        }
        drunkState = false;
    }

    void Update()
    {
        if(isDead) {
            Destroy(gameObject);
        }
    }
    void OnDisable(){
        if(gameObject){ Destroy(gameObject); }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + Time.deltaTime * playerModel.speed.Value * new Vector2(horizontal, vertical).normalized);
    }

    private void OnDestroy()
    {
        //remove all event listener
        EventMgr.Instance.RemoveEventListener("GamePaused", GlobalControlLock);
        EventMgr.Instance.RemoveEventListener("GameResumed", GlobalControlUnlock);
        EventMgr.Instance.RemoveEventListener<E_AllKeysActs>("KeyIsHeld", Controls);
        EventMgr.Instance.RemoveEventListener<E_AllKeysActs>("KeyIsReleased", CancelControls);
        EventMgr.Instance.RemoveEventListener<bool>("DrunkDrug", ChangeDrunkState);
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
                    if(drunkState) ChangeCharacterDirection((E_MoveType)drunkMovements[0]);
                    else ChangeCharacterDirection(E_MoveType.up);
                    break;
                case E_AllKeysActs.player1down:
                    if(drunkState) ChangeCharacterDirection((E_MoveType)drunkMovements[1]);
                    else ChangeCharacterDirection(E_MoveType.down);
                    break;
                case E_AllKeysActs.player1left:
                    if(drunkState) ChangeCharacterDirection((E_MoveType)drunkMovements[2]);
                    else ChangeCharacterDirection(E_MoveType.left);
                    break;
                case E_AllKeysActs.player1right:
                    if(drunkState) ChangeCharacterDirection((E_MoveType)drunkMovements[3]);
                    else ChangeCharacterDirection(E_MoveType.right);
                    break;
            }
        }
        else if (whichCharacter == 2)
        {
            switch (act)
            {
                case E_AllKeysActs.player2up:
                    if(drunkState) ChangeCharacterDirection((E_MoveType)drunkMovements[0]);
                    else ChangeCharacterDirection(E_MoveType.up);
                    break;
                case E_AllKeysActs.player2down:
                    if(drunkState) ChangeCharacterDirection((E_MoveType)drunkMovements[1]);
                    else ChangeCharacterDirection(E_MoveType.down);
                    break;
                case E_AllKeysActs.player2left:
                    if(drunkState) ChangeCharacterDirection((E_MoveType)drunkMovements[2]);
                    else ChangeCharacterDirection(E_MoveType.left);
                    break;
                case E_AllKeysActs.player2right:
                    if(drunkState) ChangeCharacterDirection((E_MoveType)drunkMovements[3]);
                    else ChangeCharacterDirection(E_MoveType.right);
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
        print("lock");
    }
    private void GlobalControlUnlock()
    {
        InputMgr.Instance.SwitchAllButtons(true);
    }
    private List<int> ShuffleList(List<int> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            int temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
        return list;
    }

    public void RefreshRopeRadius()
    {
        dj.autoConfigureDistance = false;
        dj.distance = playerModel.maxRadius.Value;
    }
    
}
