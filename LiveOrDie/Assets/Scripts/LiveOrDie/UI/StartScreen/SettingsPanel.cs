using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : BasePanel
{
    private void Start()
    {
        Slider BGMVolumeSlider = GetUIComponent<Slider>("BGMVolumeSlider");
        Slider SFXVolumeSlider = GetUIComponent<Slider>("SFXVolumeSlider");
        BGMVolumeSlider.value = AudioMgr.Instance.GetBGMVolume();
        SFXVolumeSlider.value = AudioMgr.Instance.GetSFXVolume();
    }

    protected override void OnClick(string buttonName)
    {
        switch (buttonName)
        {
            case "BackButton":
                // print("back");
                AudioMgr.Instance.PlayAudio("OnClick", false);
                UIMgr.Instance.HidePanel("SettingsPanel");
                break;
            default:
                break;
        }
    }

    protected override void OnSliderValueChange(string sliderName, float floatValue)
    {
        switch (sliderName)
        {
            case "BGMVolumeSlider":
                AudioMgr.Instance.ChangeBGMVolume(floatValue);
                break;
            case "SFXVolumeSlider":
                AudioMgr.Instance.ChangeAudioVolume(floatValue);
                break;
        }
    }

    public override void Show()
    {
        (transform as RectTransform).offsetMax= Vector2.zero;
        (transform as RectTransform).offsetMin= Vector2.zero;
        
    }
    
    
}
