using System.Collections;
using UnityEngine;

public class PeachWoodSword : MeleeWeapon
{
    private Transform player1Transform, player2Transform;
    private GameObject player1, player2;
    
    public override void Initialize() 
    {
        weaponAccuracy = 1.0f;
        weaponDamage = 1;
        weaponRate = 5.0f;
        weaponLevel = 1;
        weaponName = "PeachWoodSword";
        meleeRange = 1.0f;
        meleeSwingTime = 2.0f;

        autoAttackOn = false;

        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        player1Transform = player1.transform;
        player2Transform = player2.transform;
    }

    public override void Attack()
    {
        PoolMgr.Instance.GetObjAsync("Prefabs/Weapons/PeachWoodSwordAttack", (sword) => {
            sword.transform.position = (player1Transform.position + player2Transform.position) / 2;
            sword.transform.parent = transform;

            PWSAttackBehaviour pwsAttackBehaviour = sword.GetComponent<PWSAttackBehaviour>();
            pwsAttackBehaviour.Initialize(this);
            pwsAttackBehaviour.Fire(player1Transform, player2Transform);
        });
    }

    protected override IEnumerator AutoAttackRoutine()
    {
        while(autoAttackOn) {
            Attack();
            yield return new WaitForSeconds(weaponRate);
        }
    }

}