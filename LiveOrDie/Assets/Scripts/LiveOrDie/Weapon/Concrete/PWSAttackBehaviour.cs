using System.Collections;
using UnityEngine;

public class PWSAttackBehaviour : AttackBehaviourBase
{
    [SerializeField] public float pwsAlpha = 0.5f;
    [SerializeField] public float fadeTime = 1.0f;
    private float range;
    private float swingTime;
    private float damage;
    private Vector3 rotatePosOffset = new Vector3(0, 0.5f, 0);

    private Rigidbody2D pwsRb;
    private SpriteRenderer pwsSprite;

    public override void Initialize(Weapon weapon)
    {
        range = ((MeleeWeapon)weapon).meleeRange.Value;
        swingTime = ((MeleeWeapon)weapon).meleeSwingTime.Value;
        damage = weapon.weaponDamage.Value;
        
        pwsRb = GetComponent<Rigidbody2D>();
        pwsSprite = GetComponent<SpriteRenderer>();
        pwsSprite.color = new Color(1, 1, 1, 0);

        // Set the local scale of the transform so the length is equal to the range
        transform.localScale = new Vector3(range, range, 1);
    }

    public void Fire(Transform p1Transform, Transform p2Transform) 
    {
        if(!p1Transform || !p2Transform) return;
        StartCoroutine(fireRoutine(p1Transform, p2Transform));
    }

    private IEnumerator fireRoutine(Transform p1Transform, Transform p2Transform)
    {
        yield return swingRoutine(p1Transform, p2Transform);
        yield return FadeOutRoutine();
        PoolMgr.Instance.PushObj("Prefabs/Weapons/PeachWoodSwordAttack",this.gameObject);
    }

    private IEnumerator swingRoutine(Transform p1Transform, Transform p2Transform) 
    {
        StartCoroutine(FadeInRoutine());
        float elapsedTime = 0f;
        float startRotation = pwsRb.rotation;
        float targetRotation = startRotation - 360f; // Rotate counterclockwise by 360 degrees
        AudioMgr.Instance.PlayAudio("pws_sfx",false);
        while (elapsedTime < swingTime)
        {
            float t = elapsedTime / swingTime; // Normalized time between 0 and 1
            float currentRotation = Mathf.Lerp(startRotation, targetRotation, t);
            pwsRb.MoveRotation(currentRotation);

            // Update Rigidbody2D position to match parent's initial position
            pwsRb.MovePosition((p1Transform.position + p2Transform.position) / 2 + rotatePosOffset);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final rotation is exactly the target rotation
        pwsRb.MoveRotation(targetRotation);
        pwsRb.MovePosition((p1Transform.position + p2Transform.position) / 2 + rotatePosOffset);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy")) {
            other.GetComponent<Enemy>().TakeDamage((int)damage);
        }
    }

    private IEnumerator FadeInRoutine()
    {
        float alpha = 0;
        while(alpha < pwsAlpha) {
            alpha += Time.deltaTime / fadeTime;
            pwsSprite.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

    }

    private IEnumerator FadeOutRoutine()
    {
        float alpha = pwsAlpha;
        while(alpha > 0) {
            alpha -= Time.deltaTime / fadeTime;
            pwsSprite.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
    }


}