using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private void Start()
    {
        UIMgr.Instance.ShowPanel<StartScreenPanel>("StartScreenPanel",E_PanelLayer.Top);
    }
}
