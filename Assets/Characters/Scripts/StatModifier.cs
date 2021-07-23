[System.Serializable]
public class StatModifier
{
    public StatModifierType Type;
    public float Amount;
    public StatModifierValueType ValueType;
    public float Durability;
    public StatModifierTarget Target;
    public string Source;
    public StatModifier(StatModifierType type, float amount, StatModifierValueType valueType, float durability, StatModifierTarget target)
    {
        Type = type;
        Amount = amount;
        ValueType = valueType;
        Durability = durability;
        Target = target;
        Source = null;
    }
}
