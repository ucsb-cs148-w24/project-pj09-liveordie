using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PausePanel : BasePanel
{
    protected override void OnClick(string buttonName)
    {
        switch (buttonName)
        {
            case "SettingsButton":
                print("Open Settings Panel");
                // UIMgr.Instance.ShowPanel<SettingsPanel>("SettingsPanel", E_PanelLayer.Top); //show settings panel
                UIMgr.Instance.HidePanel("PausePanel"); // hide pause panel
                break;
            
            case "ResumeButton":
                UIMgr.Instance.HidePanel("PausePanel"); // hide pause panel
                break;
            
            case "BackToMenuButton":
                print("back to menu");
                // SceneMgr.Instance.LoadSceneAsync("StartScreen"); //load start screen
                break;
        }
    }

    public override void Show()
    {
        
    }
}
