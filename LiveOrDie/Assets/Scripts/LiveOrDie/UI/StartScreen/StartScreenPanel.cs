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
                // print("startGame");
                // GoToScene("PauseMenuTest");

                GoToScene("MainScene");
                
                UIMgr.Instance.HidePanel("StartScreenPanel");
                break;
            
            case "SettingsButton":
                AudioMgr.Instance.PlayAudio("OnClick", false);
                UIMgr.Instance.ShowPanel<SettingsPanel>("SettingsPanel", E_PanelLayer.Top);
                break;
            
            case "QuitButton":
                AudioMgr.Instance.PlayAudio("OnClick", false);
                Application.Quit(); //only works after deploying
                print("QuitGame");
                break;
            default:
                break;
        }
    }
    
    
    private void GoToScene(string sceneName)
    {
        UIMgr.Instance.ShowPanel<LoadingPanel>("LoadingPanel", E_PanelLayer.Top); //show loading panel
        PoolMgr.Instance.Clear(); //empty the pool to prevent null reference

        SceneMgr.Instance.LoadSceneAsync(sceneName, () =>
        {
            EventMgr.Instance.EventTrigger("ProgressBar", 1f);
            // EventMgr.Instance.EventTrigger("Load"+sceneName+"Completed"); //for later usage 
            
            
        }); 
    }
}

