using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float weaponAccuracy;
    public int weaponDamage;
    public float weaponRate;
    public int weaponLevel;

    public string weaponName;

    public abstract void Initialize();

    public abstract void Attack();

    public abstract void StartAutoAttack();

    public abstract void StopAutoAttack();

}