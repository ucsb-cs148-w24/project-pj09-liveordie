using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainSceneAudioController : MonoBehaviour
{
    private void Start()
    {
        AudioMgr.Instance.PlayAudio("gong",false);

        AudioMgr.Instance.PlayBGM("MainSceneBGM1");
    }

    private void OnDestroy()
    {
        AudioMgr.Instance.StopBGM();
    }

    private void Update(){

       
        
    }

}
