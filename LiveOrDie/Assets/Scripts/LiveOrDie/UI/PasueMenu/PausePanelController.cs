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
        EventMgr.Instance.AddEventListener("PausePanelLock", PausePanelLock);
        EventMgr.Instance.AddEventListener("PausePanelUnLock", PausePanelUnLock);
    }

    private bool isPausePanelShown = false;
    private bool isPausePanelLocked = false;
    private void Update()
    {
        if(isPausePanelLocked) return; //cannot open when lock is on
        ;
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

    private void PausePanelLock()
    {
        isPausePanelLocked = true;
    }

    private void PausePanelUnLock()
    {
        isPausePanelLocked = false;
    }
    
}
