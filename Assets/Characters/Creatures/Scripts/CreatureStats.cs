using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureStats : CharacterStats
{
    public float TargetRadius = 20.0f;
    public float AttackDistance = 6.0f;

    public override void Die()
    {
        // TODO: What happens when creature dies
        Debug.Log($"{Name} died.");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, TargetRadius);
    }
}
