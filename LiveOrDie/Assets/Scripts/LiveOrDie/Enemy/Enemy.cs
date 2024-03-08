using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    public int health {get; set;}
    public int damage {get; set;}
    public GameObject target {get; set;}
    protected EnemyAttack attack;
    protected PopupIndicatorFactory damageIndicatorFactory = new PopupIndicatorFactory();

    public abstract void Initialize();

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        damageIndicatorFactory.CreateAsync(transform.position, (obj) => {
            obj.transform.SetParent(transform);
            obj.GetComponentInChildren<PopupIndicator>().Initialize(damage.ToString());
        });
    }

    protected virtual void Die() {}

}
