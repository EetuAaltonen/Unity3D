using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CreatureStats))]
public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent _agent;
    private CreatureStats _creatureStats;
    private CombatController _combatController;

    private Transform _target;


    // Start is called before the first frame update
    void Start()
    {
        _creatureStats = GetComponent<CreatureStats>();
        _combatController = CombatController.Instance;

        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _creatureStats.MaxWalkingSpeed.GetValue();
        _agent.stoppingDistance = _creatureStats.AttackDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (_creatureStats.IsDead())
        {
            _agent.isStopped = true;
        } else
        {
            if (_target == null)
            {
                _target = CharacterUtilities.FindCharacterPlayer().transform;
            } else
            {
                float distance = Vector3.Distance(_target.position, transform.position);
                if (distance <= _creatureStats.TargetRadius)
                {
                    _agent.SetDestination(_target.position);
                    if (distance <= _creatureStats.AttackDistance)
                    {
                        TransformTargetUtilities.TransformFaceTarget(transform, _target, 5f);
                        // Attack
                        if (!_creatureStats.IsAttackInCooldown())
                        {
                            CharacterStats targetStats = _target.GetComponent<CharacterStats>();
                            if (targetStats != null)
                            {
                                // TODO: Fix enemy attack
                                //_combatController.AttackTarget(_creatureStats, targetStats);
                                _creatureStats.ResetAttackCooldown();
                            }
                        }
                    }
                }
            }
        }
    }
}
