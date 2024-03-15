using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneController : MonoBehaviour
{
    private void Start()
    {
        UIMgr.Instance.ShowPanel<DialoguePanel>("DialoguePanel", E_PanelLayer.Mid);
    }
    private void OnDestroy()
    {
        UIMgr.Instance.HidePanel("DialoguePannel");

    }
}