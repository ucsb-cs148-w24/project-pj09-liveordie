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
                // print("Open Settings Panel");
                UIMgr.Instance.ShowPanel<SettingsPanel>("SettingsPanel", E_PanelLayer.Top); //show settings panel
                break;
            
            case "ResumeButton":
                UIMgr.Instance.HidePanel("PausePanel"); // hide pause panel
                EventMgr.Instance.EventTrigger("GamePaused"); //**send game paused event 
                //return to the game ------------------
                
                break;
            
            case "BackToMenuButton":
                // print("back to menu");
                UIMgr.Instance.HidePanel("PausePanel");
                GoToScene("StartScreenTest"); //load start screen
                break;
        }
    }
    
    public override void Show()
    {
        (transform as RectTransform).sizeDelta= new Vector2(960,540); //same as size in prefab
    }


    private void GoToScene(string sceneName)
    {
        UIMgr.Instance.ShowPanel<LoadingPanel>("LoadingPanel", E_PanelLayer.Top); //show loading panel
        
        SceneMgr.Instance.LoadSceneAsync(sceneName, () =>
        {
            EventMgr.Instance.EventTrigger("Load"+sceneName+"Complete");
            UIMgr.Instance.HidePanel("LoadingPanel"); //hide loading panel after loading complete
        }); 
    }
    
}
