using System;
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

                UIMgr.Instance.ShowPanel<SettingsPanel>("SettingsPanel", E_PanelLayer.Top); //show settings panel
                break;
            
            case "ResumeButton":

                UIMgr.Instance.HidePanel("PausePanel"); // hide pause panel
                //return to the game ------------------
                
                break;
            
            case "BackToMenuButton":
                
                UIMgr.Instance.HidePanel("PausePanel");
                // GoToScene("StartScreenTest"); //load start screen test
                GoToScene("StartScene"); //load start screen test
                break;
        }
    }
    
    public override void Show()
    {
        (transform as RectTransform).sizeDelta= new Vector2(960,540); //same as size in prefab
        EventMgr.Instance.EventTrigger("GamePaused"); //**send game paused event 
        Time.timeScale = 0;
    }

    public override void Hide()
    {
        Time.timeScale = 1;
        EventMgr.Instance.EventTrigger("GameResumed"); //**send game paused event 
    }


    private void GoToScene(string sceneName)
    {
        UIMgr.Instance.ShowPanel<LoadingPanel>("LoadingPanel", E_PanelLayer.Top); //show loading panel
        PoolMgr.Instance.Clear(); //empty the pool to prevent null reference
        
        SceneMgr.Instance.LoadSceneAsync(sceneName, () =>
        {
            EventMgr.Instance.EventTrigger("ProgressBar", 1f);
            // EventMgr.Instance.EventTrigger("Load"+sceneName+"Completed");  //for later usage 
            
        }); 
    }
    
}
