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
                speedModifier.value = speed.Value / 2; 
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
                speedModifier.value = speed.Value *15f;
                speed.AddModifier("Drugged Speed",speedModifier);
                movement.ChangeDrunkState(true); 
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
        healthbar = gameObject.GetComponentInChildren<CharacterHealth>();
        healthbar.playerPosition = gameObject.transform;
        if (!(movement = gameObject.GetComponent<CharacterMovement>()))
            movement = gameObject.AddComponent<CharacterMovement>();
        
        EventMgr.Instance.AddEventListener("PlayerDeath", KillPlayer);
        EventMgr.Instance.AddEventListener<E_LevelUpChoice>("PlayerLevelUp", LevelUp);
    }

    void Update()
    {
        if(isDead) {
            Destroy(gameObject);
        }
    }
    public void OnDestroy(){
        EventMgr.Instance.RemoveEventListener("PlayerDeath", KillPlayer);
        EventMgr.Instance.RemoveEventListener<E_LevelUpChoice>("PlayerLevelUp", LevelUp);
    }

    private void LevelUp(E_LevelUpChoice choice) //to do 
    {
        switch (choice)
        {
            case E_LevelUpChoice.Evasion:
                speed.baseValue *= 1.1f;
                break;
            case E_LevelUpChoice.Vitality:
                maxHealth.baseValue *= 1.2f;
                healthbar.RefreshHealthUI();
                break;
            case E_LevelUpChoice.Uninhibited:
                maxRadius.baseValue += 1f;
                movement.RefreshRopeRadius();
                break;
            case E_LevelUpChoice.Regeneration:
                maxHealth.baseValue *= 1.05f;
                characterHealth.baseValue += maxHealth.baseValue / 2;
                characterHealth.baseValue = Math.Min(characterHealth.baseValue , maxHealth.baseValue);
                healthbar.RefreshHealthUI();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(choice), choice, null);
        }
    }

}
