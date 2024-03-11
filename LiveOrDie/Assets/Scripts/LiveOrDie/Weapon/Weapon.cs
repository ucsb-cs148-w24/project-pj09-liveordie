using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour
{
    public CharacterStat weaponDamage;
    public CharacterStat weaponRate;
    public int weaponLevel;
    protected StatModifier damageLevelModifier, rateLevelModifier;

    public static string weaponName = "Weapon";
    [TextArea] public static string weaponDescription = "Weapon Description";
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

    public abstract void LevelUp(E_LevelUpChoice choice);

    public abstract string GetWeaponName();
    public abstract string GetWeaponDescription();
    public abstract Sprite GetWeaponIcon();

    public abstract string GetDetailString();

}