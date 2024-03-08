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
        TMP_Text choiceText1 = GetUIComponent<TMP_Text>("ChoiceButton1Text");
        TMP_Text choiceText2 = GetUIComponent<TMP_Text>("ChoiceButton2Text");
        TMP_Text choiceText3 = GetUIComponent<TMP_Text>("ChoiceButton3Text");

        choiceText1.text = threeChoices[0].levelUpDescription;
        choiceText2.text = threeChoices[1].levelUpDescription;
        choiceText3.text = threeChoices[2].levelUpDescription;
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
        Time.timeScale = 0;
    }
    
    public override void Hide()
    {
        if (UIMgr.Instance.GetPanel<PausePanel>("PausePanel")) return;
        Time.timeScale = 1;
        EventMgr.Instance.EventTrigger("GameResumed"); //**send game paused event 
    }
}
