using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoardController : MonoBehaviour
{
    void Start()
    {
        UIMgr.Instance.ShowPanel<ScoreBoardPanel>("ScoreBoardPanel", E_PanelLayer.Mid);
    }

    private void OnDestroy()
    {
        if(UIMgr.Instance.GetPanel<ScoreBoardPanel>("ScoreBoardPanel")) UIMgr.Instance.HidePanel("ScoreBoardPanel");
    }
}
