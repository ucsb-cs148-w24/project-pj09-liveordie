using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

public class LoadingPanel : BasePanel
{
    private float progressPercent;
    private TMP_Text progressNumText;
    private Slider progressSlider;
    void Start()
    {
        //add listener
        EventMgr.Instance.AddEventListener<float>("ProgressBar", ChangeProgressNumber);
        
        //get components
        progressNumText = GetUIComponent<TMP_Text>("ProgressNumberText");
        progressSlider =  GetUIComponent<Slider>("ProgressBar");
        
        //initialize fields
        progressPercent = 0;
    }

    private void ChangeProgressNumber(float progress)
    {
        progressPercent = progress;
        progressNumText.text = progressPercent *100  + "%";
        progressSlider.value = progressPercent;

    }
}
