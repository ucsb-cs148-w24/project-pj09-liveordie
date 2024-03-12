using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum E_LevelUpChoice
{
    //character levelUps
    Evasion, Vitality, Uninhibited, Regeneration,
    //fireball levelUps
    Scorch, RapidFire,
    //incense burner levelUps
    StrongIncense, Piety, Smolder, RapidCombustion, Preach,
    //peach wood sword levelUps
    Exorcism, RapidCleave, Phantom

}

public class LevelingManager : MonoBehaviour
{
    private float expToNextLevel = 10f; 
    private float curExp = 0; //current exp
    private float levelUpMultiplier = 1.5f; //multiplier for each level
    private float exccessiveExp = 0;
    private int level = 1; //player level
    private bool isDuringLeveling = false;

    private List<LevelUpChoice> levelUpChoiceList;

    void Start()
    {
        initLevelUpChoices();
        expToNextLevel = 0.5f; //for testing, remember to change it ********************************
        curExp = 0;
        level = 1;
        EventMgr.Instance.AddEventListener("LevelUpChoiceFinished",SwitchOffLevelUpChoiceFinishedFlag);
        EventMgr.Instance.AddEventListener<float>("ExpOrbPicked", IncreaseExp);
        EventMgr.Instance.AddEventListener<List<LevelUpChoice>>("UnlockFireballLevelUpChoices",UnlockNewLevelUpChoices);
        EventMgr.Instance.AddEventListener<List<LevelUpChoice>>("UnlockPeachWoodSwordLevelUpChoices",UnlockNewLevelUpChoices);
        EventMgr.Instance.AddEventListener<List<LevelUpChoice>>("UnlockIncenseBurnerLevelUpChoices",UnlockNewLevelUpChoices);
    }

    private void OnDestroy()
    {
        EventMgr.Instance.RemoveEventListener("LevelUpChoiceFinished", SwitchOffLevelUpChoiceFinishedFlag);
        EventMgr.Instance.RemoveEventListener<float>("ExpOrbPicked", IncreaseExp);
        EventMgr.Instance.RemoveEventListener<List<LevelUpChoice>>("UnlockFireballLevelUpChoices",UnlockNewLevelUpChoices);
        EventMgr.Instance.RemoveEventListener<List<LevelUpChoice>>("UnlockPeachWoodSwordLevelUpChoices",UnlockNewLevelUpChoices);
        EventMgr.Instance.RemoveEventListener<List<LevelUpChoice>>("UnlockIncenseBurnerLevelUpChoices",UnlockNewLevelUpChoices);
    }

    private void initLevelUpChoices()
    {
        levelUpChoiceList = new List<LevelUpChoice>
        {
            new ("Evasion",
                "Increase Speed + 10%",
                () =>
                {
                    EventMgr.Instance.EventTrigger("PlayerLevelUp", E_LevelUpChoice.Evasion);
                }),
            new ("Vitality",
                "Increase Maximum Health +20%",
                () =>
                {
                    EventMgr.Instance.EventTrigger("PlayerLevelUp", E_LevelUpChoice.Vitality);
                }),
            new ("Uninhibited",
                "Increase Rope Radius +1",
                () =>
                {
                    EventMgr.Instance.EventTrigger("PlayerLevelUp", E_LevelUpChoice.Uninhibited);
                }),
            new ("Regeneration",
                "Restore Half of Max Health and Increase Maximum Health +5%",
                () =>
                {
                    EventMgr.Instance.EventTrigger("PlayerLevelUp", E_LevelUpChoice.Regeneration);
                })
        };
        //add all the initial choices
    }
    
    private void IncreaseExp(float val)
    {
        curExp += val;
        while (curExp >= expToNextLevel)
        {
            if (!isDuringLeveling)
            {
                exccessiveExp = curExp - expToNextLevel;
                LevelUp();
                curExp += exccessiveExp;
                exccessiveExp = 0;
            }
            
        }
        EventMgr.Instance.EventTrigger("ChangeExpBar",  curExp/expToNextLevel);
        EventMgr.Instance.EventTrigger("SendLevel", level); // need to be changed
    }

    private void LevelUp()
    {
        isDuringLeveling = true;
        curExp = 0;
        expToNextLevel *= levelUpMultiplier; //increase the exp needed for leveling up
        level ++;
        UIMgr.Instance.ShowPanel<LevelUpPanel>("LevelUpPanel", E_PanelLayer.Top, (panel) =>
        {
            panel.initWithThree(GenerateLevelUpChoice(3));
        });
    }

    private List<LevelUpChoice> GenerateLevelUpChoice(int choiceNum) //return wanted number of choices
    {
        List<LevelUpChoice> randomChoices = new List<LevelUpChoice>();
        int swapIndex = 0;
        for (int i = 0; i < choiceNum; i++)
        {
            int randomIndex = Random.Range(0+i, levelUpChoiceList.Count);
            randomChoices.Add(levelUpChoiceList[randomIndex]);
            
            //switch element of choice element and the first one
            var temp = levelUpChoiceList[swapIndex];
            levelUpChoiceList[swapIndex] = levelUpChoiceList[randomIndex];
            levelUpChoiceList[randomIndex] = temp;
            swapIndex++; //next time swap with the second one
        }

        return randomChoices;
    }

    private void UnlockNewLevelUpChoices(List<LevelUpChoice> choices)
    {
        levelUpChoiceList.AddRange(choices);
    }

    private void SwitchOffLevelUpChoiceFinishedFlag()
    {
        isDuringLeveling = false;
    }
    
}
