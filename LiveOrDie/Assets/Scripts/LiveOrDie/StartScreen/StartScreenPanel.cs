using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenPanel:BasePanel
{
    protected override void OnClick(string buttonName)
    {
        switch (buttonName)
        {
            case "StartGameButton":
                print("startGame");
                break;
            case "QuitButton":
                print("QuitGame");
                break;
            case "SettingsButton":
                UIMgr.Instance.ShowPanel<SettingsPanel>("SettingsPanel", E_PanelLayer.Top);
                print("settings");
                break;
            default:
                break;
        }
    }
}

