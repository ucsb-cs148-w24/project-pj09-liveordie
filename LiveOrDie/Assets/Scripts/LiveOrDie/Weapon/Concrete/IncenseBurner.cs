using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncenseBurner : StaticWeapon
{
    private Transform player1Transform, player2Transform;
    private GameObject player1, player2;

    public static new string weaponName = "Incense Burner";
    public static new string weaponDescription = "Burns the enemy and heals the players with incense";

    public override void Initialize()
    {
        weaponDamage = new CharacterStat(baseValue: 2.0f, minValue: 0.0f, maxValue: -1.0f);
        weaponRate = new CharacterStat(baseValue: 20f, minValue: 1.0f, maxValue: -1.0f);
        staticRange = new CharacterStat(baseValue: 1.0f, minValue: 0.5f, maxValue: 5.0f);
        staticRate = new CharacterStat(baseValue: 4.0f, minValue: 1.0f, maxValue: -1.0f);
        staticDuration = new CharacterStat(baseValue: 12.0f, minValue: 5.0f, maxValue: -1.0f);

        damageLevelModifier = new StatModifier(StatModifierType.Flat, 0f, StatModifierOrder.BaseModifier);
        rateLevelModifier = new StatModifier(StatModifierType.PercentMult, 100f, StatModifierOrder.BaseModifier);
        rangeLevelModifier = new StatModifier(StatModifierType.PercentMult, 100f, StatModifierOrder.BaseModifier);
        staticRateLevelModifier = new StatModifier(StatModifierType.PercentMult, 100f, StatModifierOrder.BaseModifier);
        durationLevelModifier = new StatModifier(StatModifierType.PercentMult, 100f, StatModifierOrder.BaseModifier);


        weaponLevel = 1;

        autoAttackOn = false;

        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        player1Transform = player1.transform;
        player2Transform = player2.transform;

        List<LevelUpChoice> levelUpChoices = new List<LevelUpChoice>()
        {
            new ("Strong Incense",
                "Increase Incense Burner damage +2",
                ()=>
                {
                    LevelUp(E_LevelUpChoice.StrongIncense);
                }),
            new ("Faith",
                "Increase Incense Burner Range +20%",
                ()=>
                {
                    LevelUp(E_LevelUpChoice.Faith);
                }),
            new ("Smolder",
                "Increase Incense Burner duration +30%",
                ()=>
                {
                    LevelUp(E_LevelUpChoice.Smolder);
                }),
            new ("Rapid Combustion",
                "Increase Incense Burner Attack cooldown -10%",
                ()=>
                {
                    LevelUp(E_LevelUpChoice.RapidCombustion);
                }),
            new ("Preach",
                "Increase Incense Burner Place cooldown -20%",
                ()=>
                {
                    LevelUp(E_LevelUpChoice.Preach);
                }),
        };

        EventMgr.Instance.EventTrigger("UnlockIncenseBurnerLevelUpChoices",levelUpChoices); //add available levelup choices
        // for testing purposes
        // EventMgr.Instance.AddEventListener<E_LevelUpChoice>("LevelUp", LevelUp);
    }

    private void OnDestroy()
    {
        // EventMgr.Instance.RemoveEventListener<E_LevelUpChoice>("LevelUp", LevelUp);
    }

    public override void Attack()
    {
        PoolMgr.Instance.GetObjAsync("Prefabs/Weapons/IncenseBurnerAttack", (incenseBurner) => {
            if(!incenseBurner) return;
            incenseBurner.transform.position = (player1Transform.position + player2Transform.position) / 2;
            incenseBurner.transform.rotation = Quaternion.identity;
            incenseBurner.transform.parent = transform;

            IncenseBurnerAttackBehaviour incenseBurnerAttackBehaviour = incenseBurner.GetComponent<IncenseBurnerAttackBehaviour>();
            incenseBurnerAttackBehaviour.Initialize(this);
            incenseBurnerAttackBehaviour.Fire();
        });
        
    }

    public override void LevelUp(E_LevelUpChoice choice)
    {
        weaponLevel += 1;
        switch (choice)
        {
            case E_LevelUpChoice.StrongIncense:
                damageLevelModifier.value += 2f;
                weaponDamage.AddModifier("LevelUp", damageLevelModifier);
                break;
            case E_LevelUpChoice.Faith:
                rangeLevelModifier.value *= 1.2f;
                staticRange.AddModifier("LevelUp", rangeLevelModifier);
                break;
            case E_LevelUpChoice.Smolder:
                durationLevelModifier.value *= 1.3f;
                staticDuration.AddModifier("LevelUp", durationLevelModifier);
                break;
            case E_LevelUpChoice.RapidCombustion:
                staticRateLevelModifier.value *= 0.9f;
                staticRate.AddModifier("LevelUp", staticRateLevelModifier);
                break;
            case E_LevelUpChoice.Preach:
                rateLevelModifier.value *= 0.8f;
                weaponRate.AddModifier("LevelUp", rateLevelModifier);
                break;
        }
        
        EventMgr.Instance.EventTrigger("LevelUpWeapon");
    }

    public override string GetWeaponName()
    {
        return weaponName;
    }

    public override string GetWeaponDescription()
    {
        return weaponDescription;
    }

    public override Sprite GetWeaponIcon()
    {
        return weaponIcon;
    }

    public override string GetDetailString()
    {
        return  weaponDescription + "\n" + 
                "Level: " + weaponLevel + "\n" +
                "Damage: " + weaponDamage.Value + "\n" + 
                "Cooldown: " + weaponRate.Value + "\n" + 
                "Range: " + staticRange.Value + "\n" +
                "Attack Cooldown: " + staticRate.Value + "\n" +
                "Duration: " + staticDuration.Value;
    }

}
