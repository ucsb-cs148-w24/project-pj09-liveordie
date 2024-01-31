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
                GoToScene("PauseMenuTest");
                UIMgr.Instance.HidePanel("StartScreenPanel");
                break;
            
            case "SettingsButton":
                UIMgr.Instance.ShowPanel<SettingsPanel>("SettingsPanel", E_PanelLayer.Top);
                print("settings");
                break;
            
            case "QuitButton":
                Application.Quit(); //only works after deploying
                print("QuitGame");
                break;
            default:
                break;
        }
    }
    
    
    private void GoToScene(string sceneName)
    {
        SceneMgr.Instance.LoadSceneAsync(sceneName, () =>
        {
            EventMgr.Instance.EventTrigger("Load"+sceneName+"Complete");
        }); 
    }
}

