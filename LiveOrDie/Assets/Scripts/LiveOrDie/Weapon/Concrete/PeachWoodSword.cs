using System.Collections;
using UnityEngine;

public class PeachWoodSword : MeleeWeapon
{
    private Transform player1Transform, player2Transform;
    private GameObject player1, player2;
    
    public override void Initialize() 
    {
        weaponDamage = new CharacterStat(baseValue: 1.0f, minValue: 0.0f, maxValue: -1.0f);
        weaponRate = new CharacterStat(baseValue: 5.0f, minValue: 0.01f, maxValue: -1.0f);
        meleeRange = new CharacterStat(baseValue:1.0f, minValue: 0.5f, maxValue: 5.0f);
        meleeSwingTime = new CharacterStat(baseValue:2.0f, minValue: 0.5f, maxValue: -1);

        damageLevelModifier = new StatModifier(StatModifierType.Flat, 0f, StatModifierOrder.BaseModifier);
        rateLevelModifier = new StatModifier(StatModifierType.PercentMult, 100f, StatModifierOrder.BaseModifier);
        rangeLevelModifier = new StatModifier(StatModifierType.PercentAdd, 0f, StatModifierOrder.BaseModifier);

        weaponLevel = 1;


        autoAttackOn = false;

        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        player1Transform = player1.transform;
        player2Transform = player2.transform;

        // for testing purposes
        EventMgr.Instance.AddEventListener<E_LevelUpChoice>("LevelUp", LevelUp);
    }

    private void OnDestroy()
    {
        EventMgr.Instance.RemoveEventListener<E_LevelUpChoice>("LevelUp", LevelUp);
    }

    public override void Attack()
    {
        if(!player1Transform || !player2Transform) return;
        PoolMgr.Instance.GetObjAsync("Prefabs/Weapons/PeachWoodSwordAttack", (sword) => {
            if(!sword) return;
            sword.transform.position = (player1Transform.position + player2Transform.position) / 2;
            sword.transform.parent = transform;

            PWSAttackBehaviour pwsAttackBehaviour = sword.GetComponent<PWSAttackBehaviour>();
            pwsAttackBehaviour.Initialize(this);
            pwsAttackBehaviour.Fire(player1Transform, player2Transform);
        });
    }

    public override void LevelUp(E_LevelUpChoice choice)
    {
        weaponLevel += 1;
        damageLevelModifier.value += 1;
        rateLevelModifier.value *= 0.95f;
        rangeLevelModifier.value += 5f;

        weaponDamage.AddModifier("LevelUp", damageLevelModifier);
        weaponRate.AddModifier("LevelUp", rateLevelModifier);
        meleeRange.AddModifier("LevelUp", rangeLevelModifier);
    }

    public override string GetDetailString()
    {
        return  weaponDescription + "\n" + 
                "Level: " + weaponLevel + "\n" +
                "Damage: " + weaponDamage.Value + "\n" + 
                "Cooldown: " + weaponRate.Value + "\n" + 
                "Range: " + meleeRange.Value;
    }

}