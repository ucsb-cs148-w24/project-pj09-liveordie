using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpPanel : BasePanel
{
    private List<LevelUpChoice> threeChoices;
    public void initWithThree(List<LevelUpChoice> choices) //initialize three choices
    {
        threeChoices = choices;
        TMP_Text choiceName1 = GetUIComponent<TMP_Text>("ChoiceButton1Name");
        TMP_Text choiceName2 = GetUIComponent<TMP_Text>("ChoiceButton2Name");
        TMP_Text choiceName3 = GetUIComponent<TMP_Text>("ChoiceButton3Name");

        choiceName1.text = threeChoices[0].levelUpName;
        choiceName2.text = threeChoices[1].levelUpName;
        choiceName3.text = threeChoices[2].levelUpName;
        
        TMP_Text choiceDescription1 = GetUIComponent<TMP_Text>("ChoiceButton1Description");
        TMP_Text choiceDescription2 = GetUIComponent<TMP_Text>("ChoiceButton2Description");
        TMP_Text choiceDescription3 = GetUIComponent<TMP_Text>("ChoiceButton3Description");

        choiceDescription1.text = threeChoices[0].levelUpDescription;
        choiceDescription2.text = threeChoices[1].levelUpDescription;
        choiceDescription3.text = threeChoices[2].levelUpDescription;
        
        Image choiceIcon1 = GetUIComponent<Image>("ChoiceButton1Icon");
        Image choiceIcon2 = GetUIComponent<Image>("ChoiceButton2Icon");
        Image choiceIcon3 = GetUIComponent<Image>("ChoiceButton3Icon");

        if(threeChoices[0].levelUpIcon != null) {
            choiceIcon1.gameObject.SetActive(true);
            choiceIcon1.sprite = threeChoices[0].levelUpIcon; 
        }
        else {
            choiceIcon1.gameObject.SetActive(false);
        }
        if(threeChoices[1].levelUpIcon != null) {
            choiceIcon2.gameObject.SetActive(true);
            choiceIcon2.sprite = threeChoices[1].levelUpIcon; 
        }
        else {
            choiceIcon2.gameObject.SetActive(false);
        }
        if(threeChoices[2].levelUpIcon != null) {
            choiceIcon3.gameObject.SetActive(true);
            choiceIcon3.sprite = threeChoices[2].levelUpIcon; 
        }
        else {
            choiceIcon3.gameObject.SetActive(false);
        }
    } 
    

    protected override void OnClick(string buttonName)
    {
        switch (buttonName)
        {
            case "ChoiceButton1":
                threeChoices[0].TriggerLevelUp();
                break;
            case "ChoiceButton2":
                threeChoices[1].TriggerLevelUp();
                break;
            case "ChoiceButton3":
                threeChoices[2].TriggerLevelUp();
                break;
        }
        UIMgr.Instance.HidePanel("LevelUpPanel");
    }

    //need to consider time scale with pause panel***********************************************************
    public override void Show()
    {
        EventMgr.Instance.EventTrigger("PausePanelLock");
        Time.timeScale = 0;
    }
    
    public override void Hide()
    {
        Time.timeScale = 1;
        EventMgr.Instance.EventTrigger("PausePanelUnLock");
        EventMgr.Instance.EventTrigger("LevelUpChoiceFinished");
    }
}
