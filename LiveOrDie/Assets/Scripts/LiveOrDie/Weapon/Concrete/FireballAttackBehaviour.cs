using System.Collections;
using UnityEngine;

public class FireballAttackBehaviour : AttackBehaviourBase
{
    private float speed;
    private float maxDistance;
    private float damage;

    private Rigidbody2D firebulletRb;
    private SpriteRenderer spriteRenderer;

    public override void Initialize(Weapon weapon)
    {
        this.weapon = weapon;
        speed = ((RangedWeapon)weapon).projectileSpeed.Value;
        maxDistance = ((RangedWeapon)weapon).projectileRange.Value;
        damage = weapon.weaponDamage.Value;

        firebulletRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Fire(bool flipX) 
    {
        spriteRenderer.flipX = flipX;
        Vector2 direction = new Vector2(flipX ? 1 : -1, 0); 
        firebulletRb.velocity = direction * speed;
        AudioMgr.Instance.PlayAudio("BulletSFX", false);
        StartCoroutine(DestroyAfterTime());
    }


    private IEnumerator DestroyAfterTime() {
        yield return new WaitForSeconds(maxDistance/speed);
        if(gameObject != null) {
            PoolMgr.Instance.PushObj("Prefabs/Weapons/FireBullet",this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy") || other.CompareTag("Objects")) {
            if(other.CompareTag("Enemy")) {
                other.GetComponent<Enemy>().TakeDamage((int)damage);
            }
            PoolMgr.Instance.PushObj("Prefabs/Weapons/FireBullet",this.gameObject);
        }
    }

}