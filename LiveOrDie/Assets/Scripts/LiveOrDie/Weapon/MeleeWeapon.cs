
using UnityEngine;

public abstract class MeleeWeapon : Weapon
{
    public CharacterStat meleeRange;
    public CharacterStat meleeSwingTime;

    protected StatModifier rangeLevelModifier, swingTimeLevelModifier;

}