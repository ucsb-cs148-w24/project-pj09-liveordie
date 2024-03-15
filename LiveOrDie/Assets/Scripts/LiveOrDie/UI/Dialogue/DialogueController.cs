using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIMgr.Instance.ShowPanel<DialoguePanel>("DialoguePanel", E_PanelLayer.Mid);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
