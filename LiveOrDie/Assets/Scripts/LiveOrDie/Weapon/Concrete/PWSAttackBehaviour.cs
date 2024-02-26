using System.Collections;
using UnityEngine;

public class PWSAttackBehaviour : AttackBehaviourBase
{
    private float range;
    private float swingTime;
    private int damage;
    private float accuracy;
    private Vector3 rotatePosOffset = new Vector3(0, 0.5f, 0);

    private Rigidbody2D pwsRb;
    private SpriteRenderer pwsSprite;

    public override void Initialize(Weapon weapon)
    {
        range = ((MeleeWeapon)weapon).meleeRange;
        swingTime = ((MeleeWeapon)weapon).meleeSwingTime;
        damage = weapon.weaponDamage;
        accuracy = weapon.weaponAccuracy;
        
        pwsRb = GetComponent<Rigidbody2D>();
        pwsSprite = GetComponent<SpriteRenderer>();

        // Set the local scale of the transform so the length is equal to the range
        transform.localScale = new Vector3(range, range, 1);
    }

    public void Fire(Transform p1Transform, Transform p2Transform) 
    {
        StartCoroutine(fireRoutine(p1Transform, p2Transform));
    }

    private IEnumerator fireRoutine(Transform p1Transform, Transform p2Transform)
    {
        yield return swingRoutine(p1Transform, p2Transform);
        PoolMgr.Instance.PushObj("Prefabs/Weapons/PeachWoodSwordAttack",this.gameObject);
    }

    private IEnumerator swingRoutine(Transform p1Transform, Transform p2Transform) 
    {
        float elapsedTime = 0f;
        float startRotation = pwsRb.rotation;
        float targetRotation = startRotation - 360f; // Rotate counterclockwise by 360 degrees

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
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }


}