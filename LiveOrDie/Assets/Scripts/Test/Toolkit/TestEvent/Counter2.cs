using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Counter2 : MonoBehaviour
{
    private TextMeshProUGUI countText;
    private int count = 0;
    private void Start()
    {
        countText = GetComponent<TextMeshProUGUI>();
        EventMgr.Instance.AddEventListener("HitCube", IncreaseCounter);
        
        EventMgr.Instance.AddEventListener<string>("HitCubeWithInfo", GetInfoFromCollider);
        
        // EventMgr.Instance.AddEventListener("HitCube", () =>
        // {
        //     print("this is a lambda expression");
        // });
        
        // EventMgr.Instance.AddEventListener<string>("HitCube", (info) =>
        // {
        //     print("Get info using lambda expression" + info);
        // });
    }

    
    
    
    private void IncreaseCounter()
    {
        count++;
        countText.text =   "Counter: " + count;
    }

    private void GetInfoFromCollider(string info)
    {
        print(info);
    }
    
    
}
