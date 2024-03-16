using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUnlockOrb : Drop
{
    public float expValue = 10f;
    
    protected override void TriggerEffect()
    {
        EventMgr.Instance.EventTrigger("ExpOrbPicked", expValue);
        EventMgr.Instance.EventTrigger("UnlockWeapon");
    }
    protected override void DestroySelf()
    {
        PoolMgr.Instance.PushObj("Prefabs/WeaponUnlockOrb", this.gameObject); //hide this object
    }
}
