using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            EventMgr.Instance.EventTrigger("ExpOrbPicked");
            PoolMgr.Instance.PushObj("Prefabs/Exp", this.gameObject); //hide this object
        }
        
    }
}
