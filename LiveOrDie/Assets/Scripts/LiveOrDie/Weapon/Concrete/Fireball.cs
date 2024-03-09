using UnityEngine;

public class Fireball : RangedWeapon
{
    private Transform p1FirePoint, p2FirePoint;
    private Vector3 firePointOffset = new Vector3(0,1,0);
    private SpriteRenderer p1Sprite, p2Sprite;
    private GameObject player1, player2;
    
    public override void Initialize()
    {
        weaponDamage = new CharacterStat(1f);
        weaponRate = new CharacterStat(1f);

        projectileSpeed = new CharacterStat(10.0f, 100.0f);
        projectileRange = new CharacterStat(20.0f, 200.0f);

        weaponLevel = 1;

        autoAttackOn = false;

        // this is probably a very bad way to get these
        // add a centralized player script with functions that return this info
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        p1FirePoint = player1.transform;
        p2FirePoint = player2.transform;
        p1Sprite = player1.GetComponent<SpriteRenderer>();
        p2Sprite = player2.GetComponent<SpriteRenderer>();

        // for testing purposes
        EventMgr.Instance.AddEventListener<E_LevelUpChoice>("LevelUp", LevelUp);
    }

    private void OnDestroy()
    {
        EventMgr.Instance.RemoveEventListener<E_LevelUpChoice>("LevelUp", LevelUp);
    }

    public override void Attack()
    {
        if(!p1FirePoint || !p2FirePoint) return;
        // instantiate fireball prefab (with ProjectileAttackBehaviour script) at p1FirePoint
        PoolMgr.Instance.GetObjAsync("Prefabs/Weapons/FireBullet", (firebullet) => {
            if(!firebullet) return;
            firebullet.transform.position = p1FirePoint.position + firePointOffset;
            firebullet.transform.rotation = Quaternion.identity;
            firebullet.transform.parent = transform;

            FireballAttackBehaviour fireballAttackBehaviour = firebullet.GetComponent<FireballAttackBehaviour>();
            fireballAttackBehaviour.Initialize(this);
            fireballAttackBehaviour.Fire(p1Sprite.flipX);
        });

        // instantiate fireball prefab (with ProjectileAttackBehaviour script) at p2FirePoint
        PoolMgr.Instance.GetObjAsync("Prefabs/Weapons/FireBullet", (firebullet) => {
            if(!firebullet) return;
            firebullet.transform.position = p2FirePoint.position + firePointOffset;
            firebullet.transform.rotation = Quaternion.identity;
            firebullet.transform.parent = transform;

            FireballAttackBehaviour fireballAttackBehaviour = firebullet.GetComponent<FireballAttackBehaviour>();
            fireballAttackBehaviour.Initialize(this);
            fireballAttackBehaviour.Fire(p2Sprite.flipX);
        });

    }

    public override void LevelUp(E_LevelUpChoice choice)
    {
        weaponLevel += 1;
        weaponDamage.AddModifier(new StatModifier(StatModifierType.Flat, 0.5f), 0);
        weaponRate.AddModifier(new StatModifier(StatModifierType.PercentAdd, -5f), 0);
    }

    public override string GetDetailString()
    {
        return  weaponDescription + "\n" + 
                "Level: " + weaponLevel + "\n" +
                "Damage: " + weaponDamage.Value + "\n" + 
                "Cooldown: " + weaponRate.Value + "\n" + 
                "Projectile Speed: " + projectileSpeed.Value + "\n" + 
                "Projectile Range: " + projectileRange.Value;
    }

}