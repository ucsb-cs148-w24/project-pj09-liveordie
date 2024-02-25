using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class CharacterBase : MonoBehaviour
{  
    public int whichPlayer {get; set;} // UNIQUE ID
    protected bool isDead = false; // STATUS
    protected float health {get; set;} // healthStatus
    private GameObject peer; // BEST FRIEND
    protected Rigidbody2D rb; // Physics
    protected DistanceJoint2D dj; // Physics
    public SpriteRenderer render; // PLAYER SKIN

    // REFERENCES
    protected CharacterHealth healthbar;
    protected CharacterMovement movement;

    // private CharacterWeapon weapon; --> In IMPLEMENTATION

    public abstract void Initialize();
    protected virtual void Die() {}

}
