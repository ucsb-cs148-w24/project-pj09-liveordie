using System.Collections;
using UnityEngine;

public class FireballAttackBehaviour : AttackBehaviourBase
{
    private float speed;
    private float maxDistance;
    private float damage;

    private Rigidbody2D firebulletRb;

    public override void Initialize(Weapon weapon)
    {
        speed = ((RangedWeapon)weapon).projectileSpeed.Value;
        maxDistance = ((RangedWeapon)weapon).projectileRange.Value;
        damage = weapon.weaponDamage.Value;

        firebulletRb = GetComponent<Rigidbody2D>();
    }

    public void Fire(Vector3 direction) 
    {
        firebulletRb.rotation = Quaternion.FromToRotation(Vector3.right, direction).eulerAngles.z;
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