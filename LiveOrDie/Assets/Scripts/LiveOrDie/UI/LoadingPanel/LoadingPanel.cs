using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

public class LoadingPanel : BasePanel
{
    //fields
    private float progressPercent;
    private float alpha;
    private float fadingSpeed = 3f;

    //components
    private TMP_Text progressNumText;
    private TMP_Text LoadingText;
    private Slider progressSlider;
    private Image[] allImages;

    void Start()
    {
        //add listener
        EventMgr.Instance.AddEventListener<float>("ProgressBar", ChangeProgressNumber);

        //get components
        progressNumText = GetUIComponent<TMP_Text>("ProgressNumberText");
        progressSlider =  GetUIComponent<Slider>("ProgressBar");
        LoadingText = GetUIComponent<TMP_Text>("LoadingText");
        allImages = GetComponentsInChildren<Image>();
        
        //initialize fields
        progressPercent = 0;
        alpha = 1; //start with non-transparent background
        
    }

    private void OnDestroy() //remove all the event listener
    {
        EventMgr.Instance.RemoveEventListener<float>("ProgressBar", ChangeProgressNumber);
    }

    private void ChangeProgressNumber(float progress)
    {
        progressPercent = progress;
        progressNumText.text = progressPercent *100  + "%";
        progressSlider.value = progressPercent;
        
        if (progress == 1f) //if loading completed 
        {
            FadeOut();
        }
    }
    

    private void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        while (alpha > 0)
        { 
            alpha -= Time.deltaTime * fadingSpeed;
            foreach (var image in allImages)
            {
                image.color = new Color(0, 0, 0, alpha);
                progressNumText.alpha = alpha;
                LoadingText.alpha = alpha;
            }
            yield return new WaitForSeconds(0);
        }
        UIMgr.Instance.HidePanel("LoadingPanel"); // hide itself after loading 
        
    }
    
    
    
}
