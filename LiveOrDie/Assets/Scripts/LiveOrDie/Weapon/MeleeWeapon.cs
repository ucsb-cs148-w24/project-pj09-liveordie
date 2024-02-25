
using UnityEngine;

public abstract class MeleeWeapon : Weapon
{
    
    protected float meleeRange;
    protected float meleeSwingTime;
    [SerializeField] protected GameObject meleePrefab;

}