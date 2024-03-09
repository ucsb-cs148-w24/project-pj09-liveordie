using System.Collections.Generic;


[System.Serializable]
public class CharacterStat
{
    public float baseValue;

    public float Value
    {
        get { 
            _value = GetFinalValue();
            return _value;
        }

    }

    // -1 means no max value 
    public float maxValue;
    public float _value;

    public List<StatModifier> statModifiers;

    public CharacterStat(float baseValue, float maxValue = -1)
    {
        this.baseValue = baseValue;
        this.maxValue = maxValue;
        statModifiers = new List<StatModifier>();
    }

    // order = -1 means add at the end
    public void AddModifier(StatModifier modifier, int order = -1)
    {
        if(order == -1 || order > statModifiers.Count){
            statModifiers.Add(modifier);
            return;
        } else {
            statModifiers.Insert(order, modifier);
        }
    }

    public void RemoveModifier(StatModifier modifier)
    {
        statModifiers.Remove(modifier);
    }

    private float GetFinalValue()
    {  
        float finalValue = baseValue;

        for(int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier statModifier = statModifiers[i];
            switch(statModifier.type)
            {
                case StatModifierType.Flat:
                    finalValue = finalValue + statModifier.value;
                    break;
                case StatModifierType.PercentAdd:
                    finalValue = finalValue + (baseValue * (statModifier.value / 100));
                    break;
                case StatModifierType.PercentMult:
                    finalValue = finalValue * (1f + (statModifier.value / 100));
                    break;
            }
        }
        if(finalValue > maxValue && maxValue != -1) finalValue = maxValue;
        return finalValue;
    }

}