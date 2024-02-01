using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanelManager : MonoBehaviour
{

    private bool isGameOverShown = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) //TODO: change to "if game over"
        {
            if (!isGameOverShown)
            {
                UIMgr.Instance.ShowPanel<GameOverPanel>("GameOverPanel", E_PanelLayer.Top, (panel) =>
                {
                    isGameOverShown = true;
                });
            }
            else
            {
                UIMgr.Instance.HidePanel("GameOverPanel");
                isGameOverShown = false;
            }
            
        }
    }
}
