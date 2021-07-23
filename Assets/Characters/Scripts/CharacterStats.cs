using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float maxHealth = 100;
    public float health { get; private set; }

    public Stat damage;
    public Stat armor;

    private List<StatModifier> statModifiers;

    private void Awake()
    {
        health = maxHealth;
    }

    public void ReceiveHeal(float heal)
    {
        health = Mathf.Clamp(health + heal, 0, maxHealth);
        Debug.Log($"{transform.name} received {heal} health.");
    }

    public void TakeDamage(float damage)
    {
        damage -= armor.GetValue();

        health -= Mathf.Clamp(damage, 0, int.MaxValue);
        Debug.Log($"{transform.name} takes {damage} damage.");

        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        // Overwrittable dying method
        Debug.Log($"{transform.name} dies.");
    }

    public void AddStatModifier(StatModifier statModifier)
    {
        statModifiers.Add(statModifier);
    }

    public void RemoveStatModifier(StatModifier statModifier)
    {
        statModifiers.Remove(statModifier);
    }
}
