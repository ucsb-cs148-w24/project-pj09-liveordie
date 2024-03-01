using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour
{
    public float weaponAccuracy;
    public int weaponDamage;
    public float weaponRate;
    public int weaponLevel;

    public string weaponName;

    public bool autoAttackOn;

    public abstract void Initialize();

    public abstract void Attack();

    public virtual void StartAutoAttack()
    {
        autoAttackOn = true;
        StartCoroutine(AutoAttackRoutine());
    }

    protected virtual IEnumerator AutoAttackRoutine()
    {
        while(autoAttackOn) {
            Attack();
            yield return new WaitForSeconds(weaponRate);
        }
    }

    public virtual void StopAutoAttack()
    {
        autoAttackOn = false;
        StopCoroutine(AutoAttackRoutine());
    }

}