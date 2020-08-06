public class ItemBuff
{
    public ItemBuffType Type;
    public float Amount;
    public float Durability;
    public ItemBuff(ItemBuffType type, float amount, float durability)
    {
        Type = type;
        Amount = amount;
        Durability = durability;
    }
}

public enum ItemBuffType
{
    Attack,
    Defence,
    Health,
    HealthRegen,
    Mana,
    ManaRegen
}
