using System;
using UnityEngine;
using System.Collections.Generic;
public class Player : MonoBehaviour
{
    public int whichPlayer; // UNIQUE ID
    [HideInInspector]
    public bool isDead;

    //stats fields stored in CharacterStat
    // [HideInInspector]
    public CharacterStat speed, maxRadius, characterHealth, maxHealth;
    public CharacterStat pickupRange;
    [HideInInspector]
    public StatModifier speedModifier, radiusModifier, healthModifier, maxHealthModifier;
    public StatModifier pickupRangeModifier;
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
    public SpriteRenderer render; // PLAYER SKIN
    private CircleCollider2D pickupBox;
    private bool smallScaling, bigScaling;

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
                healthbar.setSensitiveState(true);
                break;
            case "rebirth":
                // every player property (not including weapons)
                speed.RemoveModifier("LevelUp");
                maxRadius.RemoveModifier("LevelUp");
                characterHealth.RemoveModifier("LevelUp");
                maxHealth.RemoveModifier("LevelUp");
                pickupRange.RemoveModifier("LevelUp");
                healthbar.RefreshHealthUI();
                movement.RefreshRopeRadius();
                pickupBox.radius = pickupRange.Value;
                break;
            case "goliath":
                gameObject.transform.localScale *= 3;
                speedModifier.value = -(speed.Value / 2); 
                speed.AddModifier("Drugged Speed",speedModifier);
                bigScaling = true;
                break;
            case "mouse":
                gameObject.transform.localScale /= 2;
                speedModifier.value = speed.Value *1.5f;
                speed.AddModifier("Drugged Speed",speedModifier);
                smallScaling = true;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    // setter functions
    private void KillPlayer() {isDead = true;}
    public void ResetCharacteristics(){ // note health is not a reset bc it's a permanent effect
        speed.RemoveModifier("Drugged Speed"); // reset
        movement.ChangeDrunkState(false); // reset
        healthbar.setSensitiveState(false); // reset
        EventMgr.Instance.EventTrigger("MagicMushroom", false); // reset
        EventMgr.Instance.EventTrigger("Nausea", false);
        if(bigScaling == true) gameObject.transform.localScale /= 2; 
        if(smallScaling == true) gameObject.transform.localScale *= 3; 
        bigScaling = false;
        smallScaling = false;
    }
    protected void OnEnable(){
        speed = new CharacterStat(baseValue: 3.0f, minValue: 1.0f);
        maxRadius = new CharacterStat(baseValue: 5.0f, minValue: 0.0f);
        characterHealth = new CharacterStat(baseValue: 50f, minValue: 0.0f);
        maxHealth = new CharacterStat(baseValue: 50f, minValue: 0.0f);
        pickupRange = new CharacterStat(1.0f, 1.0f);
        
        speedModifier = new StatModifier(StatModifierType.Flat, 0f, StatModifierOrder.TemporaryModifier);
        radiusModifier = new StatModifier(StatModifierType.Flat, 0f, StatModifierOrder.BaseModifier);
        healthModifier = new StatModifier(StatModifierType.Flat, 0f, StatModifierOrder.BaseModifier);
        maxHealthModifier = new StatModifier(StatModifierType.PercentMult, 100f, StatModifierOrder.BaseModifier);
        pickupRangeModifier = new StatModifier(StatModifierType.Flat, 0f, StatModifierOrder.BaseModifier);

        isDead = false;
        smallScaling = false;
        bigScaling = false;
        if(!(rb = gameObject.GetComponent<Rigidbody2D>())) 
            rb = gameObject.AddComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        pickupBox = GetComponentInChildren<CircleCollider2D>();
        render = GetComponent<SpriteRenderer>();
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
                speedModifier.value *= 1.1f;
                speed.AddModifier("LevelUp", speedModifier);
                break;
            case E_LevelUpChoice.Vitality:
                maxHealth.baseValue *= 1.2f;
                healthbar.RefreshHealthUI();
                break;
            case E_LevelUpChoice.Uninhibited:
                radiusModifier.value += 1f;
                maxRadius.AddModifier("LevelUp", radiusModifier);
                movement.RefreshRopeRadius();
                break;
            case E_LevelUpChoice.Regeneration:
                maxHealth.baseValue *= 1.05f;
                characterHealth.baseValue += maxHealth.baseValue / 2;
                characterHealth.baseValue = Math.Min(characterHealth.baseValue , maxHealth.baseValue);
                healthbar.RefreshHealthUI();
                break;
            case E_LevelUpChoice.Siphon:
                pickupRangeModifier.value += 1;
                pickupRange.AddModifier("LevelUp", pickupRangeModifier);
                pickupBox.radius = pickupRange.Value;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(choice), choice, null);
        }
    }

}
