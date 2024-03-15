using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUnlockOrb : Drop
{
    protected override void DestroySelf()
    {
        PoolMgr.Instance.PushObj("Prefabs/WeaponUnlockOrb", this.gameObject); //hide this object
    }

    protected override void TriggerEffect()
    {
        EventMgr.Instance.EventTrigger("UnlockWeapon");
    }
}
