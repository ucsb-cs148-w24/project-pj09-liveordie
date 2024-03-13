using UnityEngine;

public abstract class StaticWeapon : Weapon
{
    public CharacterStat staticRange;
    public CharacterStat staticRate;
    public CharacterStat staticDuration;

    protected StatModifier rangeLevelModifier, staticRateLevelModifier, durationLevelModifier;
}