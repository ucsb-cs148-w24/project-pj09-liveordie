using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPanel : BasePanel
{
    protected override void OnClick(string buttonName)
    {
        switch (buttonName)
        {
            case "BackButton":
                print("back");
                UIMgr.Instance.HidePanel("SettingsPanel");
                break;
            default:
                break;
        }
    }
    

    
}
