using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpChoice
{
    private Action triggerAction;
    public string levelUpName;
    public string levelUpDescription;
    public Sprite levelUpIcon;

    public LevelUpChoice(string name, string description, Sprite icon, Action levelUpAction)
    {
        levelUpName = name;
        levelUpDescription = description;
        levelUpIcon = icon;
        triggerAction = levelUpAction;
    }
    

    public void TriggerLevelUp()
    {
        triggerAction();
    }
}
