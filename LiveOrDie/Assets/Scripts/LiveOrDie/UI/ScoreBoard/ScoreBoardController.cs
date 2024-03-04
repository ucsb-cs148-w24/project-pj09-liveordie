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
        if(this.gameObject.activeSelf) UIMgr.Instance.HidePanel("ScoreBoardPanel");
    }
}
