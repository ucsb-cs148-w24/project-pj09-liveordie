public enum StatModifierType
{
    Flat,
    PercentAdd,
    PercentMult
}

public class StatModifier
{
    public StatModifierType type;
    public float value;

    public StatModifier(StatModifierType type, float value)
    {
        this.type = type;
        this.value = value;
    }
}