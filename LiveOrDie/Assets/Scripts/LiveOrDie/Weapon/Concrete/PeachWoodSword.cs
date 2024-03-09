using System.Collections;
using UnityEngine;

public class PeachWoodSword : MeleeWeapon
{
    private Transform player1Transform, player2Transform;
    private GameObject player1, player2;
    
    public override void Initialize() 
    {
        weaponDamage = new CharacterStat(1);
        weaponRate = new CharacterStat(5.0f);
        meleeRange = new CharacterStat(1.0f, 5.0f);
        meleeSwingTime = new CharacterStat(2.0f);

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
        weaponDamage.AddModifier(new StatModifier(StatModifierType.Flat, 1f), 0);
        weaponRate.AddModifier(new StatModifier(StatModifierType.PercentAdd, -5f), 0);
        meleeRange.AddModifier(new StatModifier(StatModifierType.PercentAdd, 5f), 0);
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