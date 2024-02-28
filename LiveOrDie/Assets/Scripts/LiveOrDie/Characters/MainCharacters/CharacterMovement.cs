using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [HideInInspector]
    public Player player;
    private Rigidbody2D rb;
    private SpriteRenderer render;
    private float speed;
    public bool isMovementLocked = false;
    public int horizontal, vertical;

    private enum E_MoveType
    {
        up,
        down,
        left,
        right,
    }

    public float getSpeed() {return speed;}
    public void SelfDestruct() { Destroy(gameObject); }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Enemy")){ EventMgr.Instance.EventTrigger("Hit", player.whichPlayer); }
    }
    public void OnEnable(){
        speed = 10f;
        horizontal = vertical = 0;
    }
    void Start(){ 
        rb = player.getRigidBody();
        render = player.getSpriteRenderer();
        
        // Event listener
        EventMgr.Instance.AddEventListener("GamePaused", GlobalControlLock);
        EventMgr.Instance.AddEventListener("GameResumed", GlobalControlUnlock);
        
        // Event listener  control events
        EventMgr.Instance.AddEventListener<E_AllKeysActs>("KeyIsHeld", Controls);
        EventMgr.Instance.AddEventListener<E_AllKeysActs>("KeyIsReleased", CancelControls);

        //open key control lock (also create InputMgr instance)
        GlobalControlUnlock();
    }

    void Update(){ }
    void OnDisable(){ if(gameObject){ Destroy(gameObject); }}

    private void FixedUpdate()
    {
        rb.position += Time.deltaTime * speed * new Vector2(horizontal, vertical).normalized;
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
        if (player.whichPlayer == 1)
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
        else if (player.whichPlayer == 2)
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
        if (player.whichPlayer == 1)
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
        else if (player.whichPlayer == 2)
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
