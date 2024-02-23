using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBgmController : MonoBehaviour
{
    private void Start()
    {
        AudioMgr.Instance.PlayBGM("MainSceneBGM1");
    }
    
}
