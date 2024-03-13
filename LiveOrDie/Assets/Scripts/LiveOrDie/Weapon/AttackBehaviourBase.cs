using UnityEngine;

public abstract class AttackBehaviourBase : MonoBehaviour
{   
    public Weapon weapon;
    public abstract void Initialize(Weapon weapon);

}