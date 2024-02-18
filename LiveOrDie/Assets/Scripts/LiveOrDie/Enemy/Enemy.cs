using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    public int health {get; set;}
    public int damage {get; set;}
    public bool isDead {get; set;}
    public GameObject target {get; set;}

    public abstract void Initialize();

    public virtual void Kill() {
        isDead = true;
    }

}
