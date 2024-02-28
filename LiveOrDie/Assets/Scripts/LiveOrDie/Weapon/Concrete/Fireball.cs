using System.Collections;
using UnityEngine;

public class Fireball : RangedWeapon
{
    private Transform p1FirePoint, p2FirePoint;
    private Vector3 firePointOffset = new Vector3(0,1,0);
    private SpriteRenderer p1Sprite, p2Sprite;
    private GameObject player1, player2;
    private bool autoAttackOn;
    
    public override void Initialize()
    {
        weaponAccuracy = 1.0f;
        weaponDamage = 1;
        weaponRate = 1.0f;
        weaponLevel = 1;
        weaponName = "Fireball";
        projectileSpeed = 10.0f;
        projectileRange = 20.0f;

        autoAttackOn = false;

        // this is probably a very bad way to get these
        // add a centralized player script with functions that return this info
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        p1FirePoint = player1.transform;
        p2FirePoint = player2.transform;
        p1Sprite = player1.GetComponent<SpriteRenderer>();
        p2Sprite = player2.GetComponent<SpriteRenderer>();

    }

    public override void Attack()
    {
        // instantiate fireball prefab (with ProjectileAttackBehaviour script) at p1FirePoint
        PoolMgr.Instance.GetObjAsync("Prefabs/Weapons/FireBullet", (firebullet) => {
            firebullet.transform.position = p1FirePoint.position + firePointOffset;
            firebullet.transform.rotation = Quaternion.identity;
            firebullet.transform.parent = transform;

            ProjectileAttackBehaviour projectileAttackBehaviour = firebullet.GetComponent<ProjectileAttackBehaviour>();
            projectileAttackBehaviour.Initialize(this);
            projectileAttackBehaviour.Fire(p1Sprite.flipX ? Vector3.right : Vector3.left);
        });

        // instantiate fireball prefab (with ProjectileAttackBehaviour script) at p2FirePoint
        PoolMgr.Instance.GetObjAsync("Prefabs/Weapons/FireBullet", (firebullet) => {
            firebullet.transform.position = p2FirePoint.position + firePointOffset;
            firebullet.transform.rotation = Quaternion.identity;
            firebullet.transform.parent = transform;

            ProjectileAttackBehaviour projectileAttackBehaviour = firebullet.GetComponent<ProjectileAttackBehaviour>();
            projectileAttackBehaviour.Initialize(this);
            projectileAttackBehaviour.Fire(p2Sprite.flipX ? Vector3.right : Vector3.left);
        });

    }

    public override void StartAutoAttack()
    {
        autoAttackOn = true;
        StartCoroutine(AutoAttackRoutine());
    }

    private IEnumerator AutoAttackRoutine()
    {
        while(autoAttackOn) {
            Attack();
            yield return new WaitForSeconds(weaponRate);
        }
    }

    public override void StopAutoAttack()
    {
        autoAttackOn = false;
        StopCoroutine(AutoAttackRoutine());
    }
}