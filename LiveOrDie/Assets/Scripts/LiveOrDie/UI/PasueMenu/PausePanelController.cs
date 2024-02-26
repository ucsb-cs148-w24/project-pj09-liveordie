using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanelController : MonoBehaviour
{
    private void Start()
    {
        EventMgr.Instance.AddEventListener("GamePaused", SwitchPauseState);
        EventMgr.Instance.AddEventListener("GameResumed", SwitchPauseState);
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

    private void SwitchPauseState()
    {
        isPausePanelShown = !isPausePanelShown;
    }
        
}
