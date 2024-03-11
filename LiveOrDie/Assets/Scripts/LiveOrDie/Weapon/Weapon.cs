using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour
{
    public CharacterStat weaponDamage;
    public CharacterStat weaponRate;
    public int weaponLevel;
    protected StatModifier damageLevelModifier, rateLevelModifier;

    public string weaponName;
    [TextArea] public string weaponDescription;
    public Sprite weaponIcon;

    public bool autoAttackOn;
    public float cooldownTimeLeft;

    private Coroutine autoAttackRoutine;

    public abstract void Initialize();

    public abstract void Attack();

    public virtual void StartAutoAttack()
    {
        autoAttackOn = true;
        autoAttackRoutine = StartCoroutine(AutoAttackRoutine());
    }

    protected virtual IEnumerator AutoAttackRoutine()
    {
        while(autoAttackOn) {
            for(cooldownTimeLeft = weaponRate.Value; cooldownTimeLeft > 0; cooldownTimeLeft -= Time.deltaTime) {
                yield return null;
            }
            Attack();
        }
    }

    public virtual void StopAutoAttack()
    {
        autoAttackOn = false;
        StopCoroutine(autoAttackRoutine);
    }

    public abstract void LevelUp();

    public abstract string GetDetailString();

}