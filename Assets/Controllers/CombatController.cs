using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public static CombatController Instance;

    private List<string> _combatLog = new List<string>();

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AttackTarget(CharacterStats sourceStats, CharacterStats targetStats)
    {
        if (!targetStats.IsInvulnerable() && !targetStats.IsDead())
        {
            float damage = sourceStats.Damage.GetValue();
            targetStats.TakeDamage(damage);

            LogCombatAction($"{targetStats.name} takes {damage} damage.");
        }
    }

    public void HealTarget(CharacterStats sourceStats, CharacterStats targetStats)
    {
        float heal = 0;
        sourceStats.GetStatModifierByType(StatModifierType.Restoration).ForEach(x => heal += x.Amount);
        targetStats.ReceiveHeal(heal);

        LogCombatAction($"{targetStats.name} received {heal} health.");
    }

    private void LogCombatAction(string actionMessage)
    {
        Debug.Log(actionMessage);
    }
}
