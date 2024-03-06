using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpChoice
{
    private Action triggerAction;
    public string levelUpDescription;

    public LevelUpChoice(string description, Action levelUpAction)
    {
        levelUpDescription = description;
        triggerAction = levelUpAction;
    }
    

    public void TriggerLevelUp()
    {
        triggerAction();
    }
}
