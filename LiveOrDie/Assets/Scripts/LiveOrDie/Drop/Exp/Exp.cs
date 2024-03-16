using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exp : Drop
{
    private float expValue = 1f;
    public Image img;
    
    protected override void TriggerEffect()
    {
        EventMgr.Instance.EventTrigger("ExpOrbPicked", expValue);
        AudioMgr.Instance.PlayAudio("OnClick", false);
    }
    
    protected override void DestroySelf()
    {
        PoolMgr.Instance.PushObj("Prefabs/Exp", this.gameObject); //hide this object
    }
}
