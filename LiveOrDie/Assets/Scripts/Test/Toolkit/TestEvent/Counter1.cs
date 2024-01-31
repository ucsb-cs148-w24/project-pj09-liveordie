using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Counter1 : MonoBehaviour
{
    private TextMeshProUGUI countText;
    private int count = 0;
    private void Start()
    {
        countText = GetComponent<TextMeshProUGUI>();
    }

    public void IncreaseCounter()
    {
        count++;
        countText.text =   "Counter: " + count;
    }
    
    public void GetInfoFromCollider(string info)
    {
        print(info);
    }
    
}
