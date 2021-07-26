using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private EquipmentController _equipmentController;


    private void Start()
    {
        _equipmentController = EquipmentController.Instance;
        _equipmentController.onEquipmentChanged += OnEquipmentChanged;
    }

    void OnEquipmentChanged(ItemData newItem, ItemData oldItem)
    {
        if (newItem != null) {
            foreach (StatModifier statModifier in newItem.StatModifier)
            {
                switch (statModifier.Type)
                {
                    case StatModifierType.MaxHealth:
                        {
                            MaxHealth.AddModifier(statModifier);
                        }
                        break;
                    case StatModifierType.MaxMana:
                        {
                            MaxMana.AddModifier(statModifier);
                        }
                        break;
                    case StatModifierType.Damage:
                        {
                            Damage.AddModifier(statModifier);
                        }
                        break;
                    case StatModifierType.Armor:
                        {
                            Armor.AddModifier(statModifier);
                        }
                        break;
                    default:
                        {
                            AddStatModifier(statModifier);
                        }
                        break;
                }
            }
        }

        if (oldItem != null)
        {
            foreach (StatModifier statModifier in oldItem.StatModifier)
            {
                switch (statModifier.Type)
                {
                    case StatModifierType.MaxHealth:
                        {
                            MaxHealth.RemoveModifier(statModifier);
                        }
                        break;
                    case StatModifierType.MaxMana:
                        {
                            MaxMana.RemoveModifier(statModifier);
                        }
                        break;
                    case StatModifierType.Damage:
                        {
                            Damage.RemoveModifier(statModifier);
                        }
                        break;
                    case StatModifierType.Armor:
                        {
                            Armor.RemoveModifier(statModifier);
                        }
                        break;
                    default:
                        {
                            RemoveStatModifier(statModifier);
                        }
                        break;
                }
            }
        }
    }

    public override void Die()
    {
        // TODO: What happens when Player dies
        Debug.Log($"{Name} died.");
    }
}