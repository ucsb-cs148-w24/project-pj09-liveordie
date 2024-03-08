using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_LevelUpChoice
{
    IncreaseSpeed,
    IncreaseMaxHealth,
    IncreaseRopeRadius,
}

public class LevelingManager : MonoBehaviour
{
    private float expToNextLevel = 10f; 
    private float curExp = 0; //current exp
    private float levelUpMultiplier = 1.5f; //multiplier for each level
    private int level = 1; //player level

    private List<LevelUpChoice> levelUpChoiceList;

    void Start()
    {
        initLevelUpChoices();
        expToNextLevel = 0.5f; //for testing, remember to change it ********************************
        curExp = 0;
        level = 1;
        EventMgr.Instance.AddEventListener<float>("ExpOrbPicked", IncreaseExp);
    }

    private void initLevelUpChoices()
    {
        levelUpChoiceList = new List<LevelUpChoice>
        {
            new ("Increase Speed",
                () =>
                {
                    EventMgr.Instance.EventTrigger("LevelUp", E_LevelUpChoice.IncreaseSpeed);
                }),
            new ("Increase Maximum Health",
                () =>
                {
                    EventMgr.Instance.EventTrigger("LevelUp", E_LevelUpChoice.IncreaseMaxHealth);
                }),
            new ("Increase Rope Radius",
                () =>
                {
                    EventMgr.Instance.EventTrigger("LevelUp", E_LevelUpChoice.IncreaseRopeRadius);
                })
        };
        //add all the initial choices
    }
    
    private void IncreaseExp(float val)
    {
        curExp += val;
        if (curExp >= expToNextLevel)
        {
            LevelUp();
        }
        EventMgr.Instance.EventTrigger("ChangeExpBar",  curExp/expToNextLevel);
        EventMgr.Instance.EventTrigger("SendLevel", level); // need to be changed
    }

    private void LevelUp()
    {
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
        for (int i = 0; i < choiceNum; i++)
        {
            int index = Random.Range(0+i, levelUpChoiceList.Count);
            randomChoices.Add(levelUpChoiceList[index]);
            
            //switch element of choice element and the first one
            var temp = levelUpChoiceList[0];
            levelUpChoiceList[0] = levelUpChoiceList[index];
            levelUpChoiceList[index] = temp;
        }

        return randomChoices;

    }
}
