using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ExpBarPanel : BasePanel
{
    protected override void OnSliderValueChange(string sliderName, float floatValue)
    {
        if (Math.Abs(floatValue - 1f) > 0.001f) return;
        switch (sliderName)
        {
            case "ExpBarSlider":
                EventMgr.Instance.EventTrigger("LevelUp");
                break;
        }
    }
}
