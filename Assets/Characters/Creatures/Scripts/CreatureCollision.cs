using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(CreatureStats))]
public class CreatureCollision : MonoBehaviour
{
    private CombatController _combatController;
    private CreatureStats _creatureStats;

    private void Start()
    {
        _combatController = CombatController.Instance;
        _creatureStats = GetComponent<CreatureStats>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "PlayerAttack")
        {
            CharacterStats playerStats = CharacterUtilities.FindCharacterPlayer().GetComponent<CharacterStats>();
            _combatController.AttackTarget(playerStats, _creatureStats);
        }
    }
}
