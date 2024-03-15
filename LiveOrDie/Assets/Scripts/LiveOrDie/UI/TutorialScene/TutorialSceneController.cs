using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        EventMgr.Instance.AddEventListener("GoToTutorial", SwitchTutorialState);
    }
    
    private bool isTutorialPanelShown = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isTutorialPanelShown)
            {
                UIMgr.Instance.ShowPanel<TutorialPanel>("TutorialPanel", E_PanelLayer.Top, (panel) =>
                {
                    isTutorialPanelShown = true;
                });
            }
            else
            {
                UIMgr.Instance.HidePanel("PausePanel");
                isTutorialPanelShown = false;
            }
            
        }
    }

    private void SwitchTutorialState()
    {
        isTutorialPanelShown = !isTutorialPanelShown;
    }
}
