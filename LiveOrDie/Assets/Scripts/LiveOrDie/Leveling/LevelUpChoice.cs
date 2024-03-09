using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpChoice
{
    private Action triggerAction;
    public string levelUpName;
    public string levelUpDescription;

    public LevelUpChoice(string name, string description, Action levelUpAction)
    {
        levelUpName = name;
        levelUpDescription = description;
        triggerAction = levelUpAction;
    }
    

    public void TriggerLevelUp()
    {
        triggerAction();
    }
}
