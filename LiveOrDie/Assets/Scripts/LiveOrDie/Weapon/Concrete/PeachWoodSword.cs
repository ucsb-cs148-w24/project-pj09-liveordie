using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeachWoodSword : MeleeWeapon
{
    private Transform player1Transform, player2Transform;
    private GameObject player1, player2;
    
    public static new string weaponName = "Peach Wood Sword";
    public static new string weaponDescription = "A peach wood sword that swings in a circle.";

    public override void Initialize() 
    {
        weaponDamage = new CharacterStat(baseValue: 1.0f, minValue: 0.0f, maxValue: -1.0f);
        weaponRate = new CharacterStat(baseValue: 5.0f, minValue: 0.01f, maxValue: -1.0f);
        meleeRange = new CharacterStat(baseValue:1.0f, minValue: 0.5f, maxValue: 5.0f);
        meleeSwingTime = new CharacterStat(baseValue:2.0f, minValue: 0.5f, maxValue: -1);

        damageLevelModifier = new StatModifier(StatModifierType.Flat, 0f, StatModifierOrder.BaseModifier);
        rateLevelModifier = new StatModifier(StatModifierType.PercentMult, 100f, StatModifierOrder.BaseModifier);
        rangeLevelModifier = new StatModifier(StatModifierType.PercentMult, 100f, StatModifierOrder.BaseModifier);

        weaponLevel = 1;


        autoAttackOn = false;

        weaponIcon = ResMgr.Instance.Load<Sprite>("Sprites/Icons/pws_icon");

        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        player1Transform = player1.transform;
        player2Transform = player2.transform;

        List<LevelUpChoice> levelUpChoices = new List<LevelUpChoice>()
        {
            new ("Exorcism",
                "Increase Peach Wood Sword damage +2",
                weaponIcon,
                ()=>
                {
                    LevelUp(E_LevelUpChoice.Exorcism);
                }),
            new ("Rapid Cleave",
                "Reduce Peach Wood Sword cooldown -30%",
                weaponIcon,
                ()=>
                {
                    LevelUp(E_LevelUpChoice.RapidCleave);
                }),
            new ("Phantom",
                "Increase Peach Wood Sword range +20%",
                weaponIcon,
                ()=>
                {
                    LevelUp(E_LevelUpChoice.Phantom);
                }),
        };
        EventMgr.Instance.EventTrigger("UnlockPeachWoodSwordLevelUpChoices", levelUpChoices); //add available levelup choices

    }

    public override void Attack()
    {
        if(!player1Transform || !player2Transform) return;
        PoolMgr.Instance.GetObjAsync("Prefabs/Weapons/PeachWoodSwordAttack", (sword) => {
            if(!sword || !player1Transform || !player2Transform) return;
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
        switch (choice)
        {
            case E_LevelUpChoice.Exorcism:
                damageLevelModifier.value += 2;
                weaponDamage.AddModifier("LevelUp", damageLevelModifier);
                break;
            case E_LevelUpChoice.RapidCleave:
                rateLevelModifier.value *= 0.7f;
                weaponRate.AddModifier("LevelUp", rateLevelModifier);
                break;
            case E_LevelUpChoice.Phantom:
                rangeLevelModifier.value *= 1.2f;
                meleeRange.AddModifier("LevelUp", rangeLevelModifier);
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
                "Range: " + meleeRange.Value;
    }

}