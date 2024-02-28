using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    public int health {get; set;}
    public int damage {get; set;}
    public GameObject target {get; set;}

    public abstract void Initialize();

    public abstract void TakeDamage(int damage);

    protected virtual void Die() {}

}
