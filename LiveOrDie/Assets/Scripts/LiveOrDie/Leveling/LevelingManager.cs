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
    void Start()
    {
        expToNextLevel = 0.5f; //remember to change it ********************************
        curExp = 0;
        level = 1;
        EventMgr.Instance.AddEventListener<float>("ExpOrbPicked", IncreaseExp);
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
        EventMgr.Instance.EventTrigger("LevelUp", GenerateLevelUpChoice());
    }

    private E_LevelUpChoice GenerateLevelUpChoice()
    {
        return (E_LevelUpChoice)Random.Range(0, 3);
    }
}
