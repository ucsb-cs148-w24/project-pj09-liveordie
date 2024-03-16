using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsPanel : BasePanel
{
    protected override void OnClick(string buttonName)
    {
        switch (buttonName)
        {
            case "BackButton":
                //AudioMgr.Instance.PlayAudio("OnClick", false);
                UIMgr.Instance.HidePanel("CreditsPanel");
                break;
            default:
                break;
        }
    }
}
