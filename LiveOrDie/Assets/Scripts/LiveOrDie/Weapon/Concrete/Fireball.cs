using UnityEngine;

public class Fireball : RangedWeapon
{
    private Transform p1FirePoint, p2FirePoint;
    private Vector3 firePointOffset = new Vector3(0,1,0);
    private SpriteRenderer p1Sprite, p2Sprite;
    private GameObject player1, player2;
    
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
        if(!p1FirePoint || !p2FirePoint) return;
        // instantiate fireball prefab (with ProjectileAttackBehaviour script) at p1FirePoint
        PoolMgr.Instance.GetObjAsync("Prefabs/Weapons/FireBullet", (firebullet) => {
            firebullet.transform.position = p1FirePoint.position + firePointOffset;
            firebullet.transform.rotation = Quaternion.identity;
            firebullet.transform.parent = transform;

            FireballAttackBehaviour fireballAttackBehaviour = firebullet.GetComponent<FireballAttackBehaviour>();
            fireballAttackBehaviour.Initialize(this);
            fireballAttackBehaviour.Fire(p1Sprite.flipX ? Vector3.right : Vector3.left);
        });

        // instantiate fireball prefab (with ProjectileAttackBehaviour script) at p2FirePoint
        PoolMgr.Instance.GetObjAsync("Prefabs/Weapons/FireBullet", (firebullet) => {
            firebullet.transform.position = p2FirePoint.position + firePointOffset;
            firebullet.transform.rotation = Quaternion.identity;
            firebullet.transform.parent = transform;

            FireballAttackBehaviour fireballAttackBehaviour = firebullet.GetComponent<FireballAttackBehaviour>();
            fireballAttackBehaviour.Initialize(this);
            fireballAttackBehaviour.Fire(p2Sprite.flipX ? Vector3.right : Vector3.left);
        });

    }

}