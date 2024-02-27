using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainSceneAudioController : MonoBehaviour
{
    private void Start()
    {
        AudioMgr.Instance.PlayBGM("MainSceneBGM2");
    }

    private void OnDestroy()
    {
        AudioMgr.Instance.StopBGM();
    }

    private void Update(){

        if(Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.M))
        AudioMgr.Instance.PlayAudio("BulletSFX",false);
        
    }

}
