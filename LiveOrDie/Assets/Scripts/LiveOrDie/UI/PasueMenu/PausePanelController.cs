using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanelController : MonoBehaviour
{
    private void Start()
    {
        EventMgr.Instance.AddEventListener("GamePaused", SwitchPauseStateOn);
        EventMgr.Instance.AddEventListener("GameResumed", SwitchPauseStateOff);
    }

    private bool isPausePanelShown = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPausePanelShown)
            {
                UIMgr.Instance.ShowPanel<PausePanel>("PausePanel", E_PanelLayer.Top, (panel) =>
                {
                    isPausePanelShown = true;
                });
            }
            else
            {
                UIMgr.Instance.HidePanel("PausePanel");
                isPausePanelShown = false;
            }
            
        }
    }

    private void SwitchPauseStateOn()
    {
        isPausePanelShown = true;
    }
    
    private void SwitchPauseStateOff()
    {
        isPausePanelShown = false;
    }
        
}
