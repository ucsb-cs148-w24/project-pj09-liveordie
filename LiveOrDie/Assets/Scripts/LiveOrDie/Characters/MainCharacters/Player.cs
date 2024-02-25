using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public abstract class Player : CharacterBase
{
    protected Player peer;
    public override void Initialize() {

        //subcomponents
        peer = GetComponent<Player>();
        render = GetComponent<SpriteRenderer>();
        
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        dj = gameObject.AddComponent<DistanceJoint2D>();
        dj.connectedBody = peer.GetComponent<Rigidbody2D>();

        health = 50f;
        healthbar = GetComponentInChildren<CharacterHealth>();
        healthbar.setHealth(health);
        // healthbar.player = this; //assign itself to its sub component
        // healthbar.Initialize();

        movement = GetComponentInChildren<CharacterMovement>();
        // movement.player = this;
        // movement.Initialize();
    }

}
