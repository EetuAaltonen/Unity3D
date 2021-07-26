using System;

[System.Serializable]
public class StatModifier
{
    public StatModifierType Type;
    public float Amount;
    public StatModifierValueType ValueType;
    public float Duration = -1;
    public StatModifierTarget Target;
    [NonSerialized]
    public string Source;
    public StatModifier(StatModifierType type, float amount, StatModifierValueType valueType, float durability, StatModifierTarget target)
    {
        Type = type;
        Amount = amount;
        ValueType = valueType;
        Duration = durability;
        Target = target;
        Source = null;
    }
}
