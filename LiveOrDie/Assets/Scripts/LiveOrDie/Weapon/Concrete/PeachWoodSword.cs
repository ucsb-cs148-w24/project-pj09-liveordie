
public class PeachWoodSword : MeleeWeapon
{
    public override void Initialize() 
    {
        weaponAccuracy = 1.0f;
        weaponDamage = 1;
        weaponRate = 5.0f;
        weaponLevel = 1;
        weaponName = "PeachWoodSword";
        meleeRange = 5.0f;
        meleeSwingTime = 0.5f;
    }

    public override void Attack()
    {
        
    }

    public override void StartAutoAttack()
    {

    }

    public override void StopAutoAttack()
    {
        
    }


}