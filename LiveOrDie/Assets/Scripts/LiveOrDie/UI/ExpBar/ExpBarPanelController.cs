using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBarPanelController : MonoBehaviour
{
    void Start()
    {
        UIMgr.Instance.ShowPanel<ExpBarPanel>("ExpBarPanel", E_PanelLayer.Mid);
    }

    private void OnDestroy()
    {
        if(UIMgr.Instance.GetPanel<ExpBarPanel>("ExpBarPanel")) UIMgr.Instance.HidePanel("ExpBarPanel");
    }
}
