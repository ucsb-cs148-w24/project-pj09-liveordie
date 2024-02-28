
using UnityEngine;

public abstract class MeleeWeapon : Weapon
{
    public float meleeRange;
    public float meleeSwingTime;
    [SerializeField] protected GameObject meleePrefab;

}