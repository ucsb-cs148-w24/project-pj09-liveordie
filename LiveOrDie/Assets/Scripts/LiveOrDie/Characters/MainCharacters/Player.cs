using System;
using UnityEngine;
using System.Collections.Generic;
public class Player : MonoBehaviour
{
    public int whichPlayer; // UNIQUE ID
    [HideInInspector]
    public bool isDead;

    //stats fields stored in CharacterStat
    [HideInInspector]
    public CharacterStat speed;
    [HideInInspector]
    public CharacterStat maxRadius;
    [HideInInspector]
    public CharacterStat characterHealth; // maybe not needed
    [HideInInspector]
    public CharacterStat maxHealth;
    
    //some other possible stats
    //cooldown
    //pickRange
    //attackRange
    //attackDamage
    //attackSpeed
    //defense
    private Player peer; // BEST FRIEND
    private Rigidbody2D rb; // Physics
    private DistanceJoint2D dj; // Physics
    private SpriteRenderer render; // PLAYER SKIN

    // REFERENCES
    [HideInInspector]
    public CharacterHealth healthbar;
    private CharacterMovement movement;
    // public functions
    public void EnforceSensitiveState(bool enforce){ healthbar.setSensitiveState(enforce); }
    public void EnforceDrunkEffect(bool enforce) {  movement.ChangeDrunkState(enforce); }
    public void EnforceHealthEffect(string type) { 
        if(type == "drop") healthbar.DecreaseHealth((int)characterHealth.Value / 2);
        else if(type == "boost") healthbar.IncreaseHealth((int)maxHealth.Value);
        else Debug.Log("Wrong use of Event Listener for EnforceHealthEffect()");
    }
    public void EnforceSpeedEffect(string type) { 
        if(type == "drop") speed.AddModifier(new StatModifier(StatModifierType.Flat, speed.Value/2f), -1);
        else if(type == "boost")speed.AddModifier(new StatModifier(StatModifierType.Flat, speed.Value*1.5f), -1);
        else if(type == "berzerkers") speed.AddModifier(new StatModifier(StatModifierType.Flat, speed.Value*15f), -1);
        else Debug.Log("Wrong use of Event Listener for EnforceSpeedEffect()");
    }

    // setter functions
    private void KillPlayer() {isDead = true;}
    public void ResetCharacteristics(){
        speed.statModifiers.Clear();
        movement.ChangeDrunkState(false);
        healthbar.setSensitiveState(false);
        EventMgr.Instance.EventTrigger("MagicMushroom", false); // reset
    }
    protected void OnEnable(){
        speed = new CharacterStat(3f);
        maxRadius = new CharacterStat(5f);
        characterHealth = new CharacterStat(50f);
        maxHealth = new CharacterStat(50f);
        isDead = false;
        if(!(rb = gameObject.GetComponent<Rigidbody2D>())) 
            rb = gameObject.AddComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    protected void Start() {
        switch (whichPlayer){
            case 1: // User 1 finds User 2 (right)
                peer = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>();
                dj = gameObject.AddComponent<DistanceJoint2D>();
                dj.connectedBody = peer.GetComponent<Rigidbody2D>();
                dj.distance = maxRadius.Value;
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
