using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartSceneAudioController : MonoBehaviour
{
    private void Start()
    {
        AudioMgr.Instance.PlayBGM("MainSceneBGM");
    }

    private void OnDestroy()
    {
        AudioMgr.Instance.StopBGM();
    }
}
