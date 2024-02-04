using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenController : MonoBehaviour
{
    private void Start()
    {
        UIMgr.Instance.ShowPanel<StartScreenPanel>("StartScreenPanel",E_PanelLayer.Mid);
    }
    
}
