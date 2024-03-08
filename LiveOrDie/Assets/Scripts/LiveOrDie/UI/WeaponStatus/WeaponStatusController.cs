using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStatusController : MonoBehaviour
{
    void Start()
    {
        UIMgr.Instance.ShowPanel<WeaponStatusPanel>("WeaponStatusPanel", E_PanelLayer.Mid);
    }

    void OnDestroy()
    {
        if(UIMgr.Instance.GetPanel<WeaponStatusPanel>("WeaponStatusPanel")) UIMgr.Instance.HidePanel("WeaponStatusPanel");
    }
}
