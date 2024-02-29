using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncenseBurner : StaticWeapon
{
    private Transform player1Transform, player2Transform;
    private GameObject player1, player2;

    public override void Initialize()
    {
        weaponAccuracy = 1.0f;
        weaponDamage = 2;
        weaponRate = 20.0f;
        weaponLevel = 1;
        weaponName = "IncenseBurner";
        staticRange = 1.0f;
        staticRate = 4.0f;
        staticDuration = 12.0f;

        autoAttackOn = false;

        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        player1Transform = player1.transform;
        player2Transform = player2.transform;
    }

    public override void Attack()
    {
        PoolMgr.Instance.GetObjAsync("Prefabs/Weapons/IncenseBurnerAttack", (incenseBurner) => {
            incenseBurner.transform.position = (player1Transform.position + player2Transform.position) / 2;
            incenseBurner.transform.rotation = Quaternion.identity;
            incenseBurner.transform.parent = transform;

            IncenseBurnerAttackBehaviour incenseBurnerAttackBehaviour = incenseBurner.GetComponent<IncenseBurnerAttackBehaviour>();
            incenseBurnerAttackBehaviour.Initialize(this);
            incenseBurnerAttackBehaviour.Fire();
        });
        
    }

}
