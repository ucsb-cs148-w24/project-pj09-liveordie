public enum StatModifierType
{
    Flat,
    PercentAdd,
    PercentMult
}

public enum StatModifierOrder
{
    BaseModifier = 0,
    PermanentModifier = 1,
    TemporaryModifier = 2
}

public class StatModifier
{
    public StatModifierType type;
    public float value;
    public StatModifierOrder order;

    public StatModifier(StatModifierType type, float value, StatModifierOrder order = StatModifierOrder.TemporaryModifier)
    {
        this.type = type;
        this.value = value;
        this.order = order;
    }
}