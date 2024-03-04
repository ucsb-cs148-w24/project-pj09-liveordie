using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class ExpBarPanel : BasePanel
{
    private Slider expBarSlider;
    
    //values below should be moved into a leveling system*************************************
    private float playerExp = 0;
    private float playerExpLimit = 10f;

    private void Start()
    {
        expBarSlider = GetUIComponent<Slider>("ExpBarSlider");
        
        //event parameter value of the exp orb (for now)
        EventMgr.Instance.AddEventListener<float>("ChangeExpBar", IncreaseExpBarValue);
    }

    protected override void OnSliderValueChange(string sliderName, float floatValue)
    {
        //could add some visual effects on the slider when exp increases
    }

    private void IncreaseExpBarValue(float val)
    {
        expBarSlider.value = val;
    }
    
}
