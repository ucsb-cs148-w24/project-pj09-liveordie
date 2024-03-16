using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TutorialPanel : BasePanel
{
    protected override void OnClick(string buttonName)
    {
        switch (buttonName)
        {
            case "BackButton":
                print("back");
                //AudioMgr.Instance.PlayAudio("OnClick", false);
                UIMgr.Instance.HidePanel("TutorialPanel");
                break;
            default:
                break;
        }
    }
    
}
