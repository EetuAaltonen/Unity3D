using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public string Name;

    // Attributes
    public Stat MaxHealth;
    public float Health { get; private set; }
    public Stat HealthRegeneration;
    public Stat MaxMana;
    public float Mana { get; private set; }
    public Stat ManaRegeneration;
    public Stat MaxStamina;
    public float Stamina { get; private set; }
    public Stat StaminaRegeneration;

    public Stat MaxWalkingSpeed;
    public Stat MaxRunningSpeed;

    // Offensive
    public Stat Damage;
    public Stat AttackSpeed;
    // Defensive
    public Stat Armor;

    public List<StatModifier> StatModifiers;

    private const float INVULNERABILITY_DELAY = 4.0f;

    private float _attackCooldown = 0.0f;
    private float _invulnerabilityTimer = 0.0f;

    private void Awake()
    {
        Health = MaxHealth.GetValue();
        Mana = MaxMana.GetValue();
    }

    // Update is called once per frame
    void Update()
    {
        if (_invulnerabilityTimer > 0.0f)
        {
            _invulnerabilityTimer -= Time.deltaTime;
        }

        if (_attackCooldown > 0.0f)
        {
            _attackCooldown -= Time.deltaTime;
        }
    }

    public bool IsInvulnerable()
    {
        return (_invulnerabilityTimer > 0.0f);
    }

    public void ResetInvulnerabilityTimer()
    {
        _invulnerabilityTimer = INVULNERABILITY_DELAY;
    }

    public void TakeDamage(float damage)
    {
        damage -= Armor.GetValue();
        Health -= Mathf.Clamp(damage, 0.0f, float.MaxValue);

        if (Health <= 0.0f)
        {
            Die();
        } else
        {
            ResetInvulnerabilityTimer();
        }
    }

    public void ReceiveHeal(float heal)
    {
        Health = Mathf.Clamp(Health + heal, 0.0f, MaxHealth.GetValue());
    }

    public bool IsDead()
    {
        return Health <= 0.0f;
    }

    public virtual void Die()
    {
        // Overwrittable dying method
        Debug.Log($"{transform.name} dies.");
    }

    public void ResetAttackCooldown()
    {
        _attackCooldown = 1 / AttackSpeed.GetValue();
    }

    public bool IsAttackInCooldown()
    {
        return (_attackCooldown > 0.0f);
    }

    public float GetAttackCooldown()
    {
        return _attackCooldown;
    }

    public List<StatModifier> GetAllStatModifier()
    {
        return StatModifiers;
    }

    public List<StatModifier> GetStatModifierByType(StatModifierType statModifierType)
    {
        return StatModifiers.Where(x => x.Type == statModifierType).ToList();
    }

    public void AddStatModifier(StatModifier statModifier)
    {
        StatModifiers.Add(statModifier);
    }

    public void RemoveStatModifier(StatModifier statModifier)
    {
        StatModifiers.Remove(statModifier);
    }
}
