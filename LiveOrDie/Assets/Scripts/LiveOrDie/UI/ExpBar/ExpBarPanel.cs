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
        EventMgr.Instance.AddEventListener<float>("ExpOrbPicked", IncreaseExpBarValue);
    }

    protected override void OnSliderValueChange(string sliderName, float floatValue)
    {
        //could add some visual effects on the slider when exp increases
    }

    private void IncreaseExpBarValue(float val)
    {
        //eventually the val should be calculated inside leveling system and sent over here************
        playerExp += val;
        expBarSlider.value = playerExp / playerExpLimit;
        EventMgr.Instance.EventTrigger("SendExp", playerExp);

        //*************************
        
        if (Math.Abs(expBarSlider.value - 1f) < 0.001f)
        {
            EventMgr.Instance.EventTrigger("LevelUp"); //level up and empty the exp bar
            expBarSlider.value = 0;
            playerExp = 0;
            
            //eventually the new exp needed should be calculated inside leveling system************
            playerExpLimit *= 1.5f;
        }
    }
    
}
