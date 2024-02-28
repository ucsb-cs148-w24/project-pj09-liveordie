
using UnityEngine;

public abstract class RangedWeapon : Weapon 
{
    public float projectileSpeed;
    public float projectileRange;

    [SerializeField] protected GameObject projectilePrefab;
}