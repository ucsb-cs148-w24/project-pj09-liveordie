using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncenseBurner : StaticWeapon
{
    private Transform player1Transform, player2Transform;
    private GameObject player1, player2;

    public override void Initialize()
    {
        weaponDamage = new CharacterStat(2.0f);
        weaponRate = new CharacterStat(20.0f);
        staticRange = new CharacterStat(1.0f, 5.0f);
        staticRate = new CharacterStat(4.0f);
        staticDuration = new CharacterStat(12.0f);

        weaponLevel = 1;

        autoAttackOn = false;

        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        player1Transform = player1.transform;
        player2Transform = player2.transform;

        // for testing purposes
        EventMgr.Instance.AddEventListener("LevelUp", LevelUp);
    }

    private void OnDestroy()
    {
        EventMgr.Instance.RemoveEventListener("LevelUp", LevelUp);
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

    public override void LevelUp()
    {
        weaponLevel += 1;
        weaponDamage.AddModifier(new StatModifier(StatModifierType.Flat, 1f), 0);
        weaponRate.AddModifier(new StatModifier(StatModifierType.PercentAdd, -5f), 0);
        staticRange.AddModifier(new StatModifier(StatModifierType.PercentAdd, 5f), 0);
        staticRate.AddModifier(new StatModifier(StatModifierType.PercentAdd, -5f), 0);
        staticDuration.AddModifier(new StatModifier(StatModifierType.PercentAdd, 5f), 0);
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
