using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncenseBurnerAttackBehaviour : AttackBehaviourBase
{

    [SerializeField] public float fadeInTime = 0.3f;
    [SerializeField] public float fadeOutTime = 3.0f;
    [SerializeField] public float radiusAlpha = 0.3f;
    private float range;
    private float rate;
    private float duration;
    private float damage;

    private Collider2D incenseBurnerRadiusCollider;
    private SpriteRenderer incenseBurnerRadiusSprite;
    
    public override void Initialize(Weapon weapon)
    {
        range = ((StaticWeapon)weapon).staticRange.Value;
        rate = ((StaticWeapon)weapon).staticRate.Value;
        duration = ((StaticWeapon)weapon).staticDuration.Value;
        damage = weapon.weaponDamage.Value;

        incenseBurnerRadiusCollider = transform.Find("IncenseBurnerRadius").GetComponent<Collider2D>();
        incenseBurnerRadiusSprite = transform.Find("IncenseBurnerRadius").GetComponent<SpriteRenderer>();

        incenseBurnerRadiusSprite.transform.localScale = new Vector3(range, range, 1);
    }

    public void Fire()
    {
        StartCoroutine(FireRoutine());
        StartCoroutine(DestroyRoutine());
    }

    private IEnumerator FireRoutine()
    {
        while(true) {
            yield return FadeInRoutine();
            TriggerDamageHealInRadius();
            StartCoroutine(FadeOutRoutine());
            yield return new WaitForSeconds(rate);
        }
    }

    private void TriggerDamageHealInRadius()
    {
        List<Collider2D> results = new List<Collider2D>();
        Physics2D.OverlapCollider(incenseBurnerRadiusCollider, new ContactFilter2D().NoFilter(), results);
        foreach(Collider2D collider in results) {
            if(collider.CompareTag("Enemy")) {
                collider.GetComponent<Enemy>().TakeDamage((int)damage);
            }
            if(collider.CompareTag("Player1") || collider.CompareTag("Player2"))
            {
                CharacterHealth health = collider.GetComponentInChildren<CharacterHealth>();
                health.IncreaseHealth((int)damage);
                health.RefreshHealthUI();
            }
        }
    }

    private IEnumerator FadeInRoutine()
    {
        float alpha = 0;
        while(alpha < radiusAlpha) {
            alpha += Time.deltaTime / fadeInTime;
            incenseBurnerRadiusSprite.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
    }

    private IEnumerator FadeOutRoutine()
    {
        float alpha = radiusAlpha;
        while(alpha > 0) {
            alpha -= Time.deltaTime / fadeOutTime;
            incenseBurnerRadiusSprite.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
    }

    private IEnumerator DestroyRoutine()
    {
        yield return new WaitForSeconds(duration);
        PoolMgr.Instance.PushObj("Prefabs/Weapons/IncenseBurnerAttack",this.gameObject);
    }


}
