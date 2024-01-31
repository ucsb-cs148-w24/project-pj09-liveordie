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

    public override void Show()
    {
        (transform as RectTransform).offsetMax= Vector2.zero;
        (transform as RectTransform).offsetMin= Vector2.zero;
    }
}
