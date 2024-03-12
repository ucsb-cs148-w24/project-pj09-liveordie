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
    public CharacterStat speed, maxRadius, characterHealth, maxHealth;
    [HideInInspector]
    public StatModifier speedModifier, radiusModifier, healthModifier, maxHealthModifier;
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
    public void EnforcePlayerEffect(string type){
        int amount;
        Debug.Log(type);
        switch (type){
            case "drop health": // no need for modifier since drug drops health permanantly
                amount = (int) (characterHealth.Value / 2);
                healthbar.DecreaseHealth(amount);
                break;
            case "boost health": // no need for modifier since drug boosts health permanantly
                amount = (int) (maxHealth.Value - characterHealth.Value);
                healthbar.IncreaseHealth(amount);
                break;
            case "drop speed":
                speedModifier.value = -(speed.Value / 2); 
                speed.AddModifier("Drugged Speed",speedModifier);
                break;
            case "boost speed":
                speedModifier.value = speed.Value *1.5f;
                speed.AddModifier("Drugged Speed",speedModifier);
                break;
            case "sensitive":
                healthbar.setSensitiveState(true);
                break;
            case "drunk":
                speedModifier.value = 10f;
                speed.AddModifier("Drugged Speed",speedModifier);
                movement.ChangeDrunkState(true); 
                break;
            case "nausea":
                break;
            default:
                break;
        }
    }

    // setter functions
    private void KillPlayer() {isDead = true;}
    public void ResetCharacteristics(){ // note health is not a reset bc it's a permanent effect
        speed.RemoveModifier("Drugged Speed"); // reset
        movement.ChangeDrunkState(false); // reset
        healthbar.setSensitiveState(false); // reset
        EventMgr.Instance.EventTrigger("MagicMushroom", false); // reset
    }
    protected void OnEnable(){
        speed = new CharacterStat(baseValue: 3.0f, minValue: 1.0f);
        maxRadius = new CharacterStat(baseValue: 5.0f, minValue: 0.0f);
        characterHealth = new CharacterStat(baseValue: 50f, minValue: 0.0f);
        maxHealth = new CharacterStat(baseValue: 50f, minValue: 0.0f);
        
        speedModifier = new StatModifier(StatModifierType.Flat, speed.baseValue, StatModifierOrder.TemporaryModifier);
        radiusModifier = new StatModifier(StatModifierType.Flat, maxRadius.baseValue, StatModifierOrder.BaseModifier);
        healthModifier = new StatModifier(StatModifierType.Flat, characterHealth.baseValue, StatModifierOrder.BaseModifier);
        maxHealthModifier = new StatModifier(StatModifierType.Flat, maxHealth.baseValue, StatModifierOrder.BaseModifier);

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
