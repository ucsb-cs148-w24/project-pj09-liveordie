using System.Collections.Generic;


[System.Serializable]
public class CharacterStat
{
    public float baseValue;
    private float oldBaseValue;

    public float Value
    {
        get { 
            if (isDirty || oldBaseValue != baseValue) {
                _value = CalculateValue();
                isDirty = false;
            }
            return _value;
        }

    }

    // -1 means no min/max value 
    public float minValue;
    public float maxValue;
    public float _value;

    private bool isDirty = true;

    public Dictionary<string, StatModifier> statModifiers;

    public CharacterStat(float baseValue, float minValue = -1, float maxValue = -1)
    {
        this.baseValue = baseValue;
        this.minValue = minValue;
        this.maxValue = maxValue;
        statModifiers = new Dictionary<string, StatModifier>();
    }

    public void AddModifier(string name, StatModifier modifier)
    {
        isDirty = true;
        if(!statModifiers.ContainsKey(name)){
            statModifiers.Add(name, modifier);
        } else {
            statModifiers[name] = modifier;
        }
    }

    public void RemoveModifier(string name)
    {
        isDirty = true;
        statModifiers.Remove(name);
    }

    public void ClearModifiers()
    {
        isDirty = true;
        statModifiers.Clear();
    }

    private float CalculateValue()
    {  
        float finalValue = baseValue;
        float sumFlat = 0;
        float sumPercentAdd = 0;
        float prodPercentMult = 1;

        // convert dictionary to list, then sort by StatModifier.order
        List<StatModifier> sortedModifiers = new List<StatModifier>(statModifiers.Values);
        sortedModifiers.Sort((a, b) => a.order.CompareTo(b.order));

        for(int i = 0; i < sortedModifiers.Count; i++)
        {
            StatModifier statModifier = sortedModifiers[i];

            // accumulate the modifiers
            switch(statModifier.type)
            {
                case StatModifierType.Flat:
                    sumFlat += statModifier.value;
                    break;
                case StatModifierType.PercentAdd:
                    sumPercentAdd += statModifier.value;
                    break;
                case StatModifierType.PercentMult:
                    prodPercentMult *= statModifier.value / 100;
                    break;
            }

            // apply the modifiers when we hit the end of the list or we hit a different order
            // applies in order: flat, then percent add, then percent mult
            if(i + 1 >= sortedModifiers.Count || sortedModifiers[i + 1].order != statModifier.order)
            {
                finalValue = finalValue + sumFlat;
                finalValue = finalValue * (1 + sumPercentAdd / 100);
                finalValue = finalValue * prodPercentMult;
            }

        }
        if(finalValue > maxValue && maxValue != -1) finalValue = maxValue;
        if(finalValue < minValue && minValue != -1) finalValue = minValue;
        return finalValue;
    }

}