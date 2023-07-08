using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public enum AIType
{
    Monster,
    Hero,
    BossMonster,
}

public enum AIState
{
    Idle,
    Moving,
    TargetingHero,
    TargetingPlayer,
    TargetingMonster,
}


[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAIComponent : MonoBehaviour
{
    public AIType AIType;

    public float DistanceCheckTime = 1f;
    public float RetryThinkTime = 10f;

    public float DistanceToTargetHero = 40f;
    public float DistanceToTargetPlayer = 40f;
    public float DistanceToTargetMonster = 40f;
    public float MaxRandomMoveDistance = 10f;

    private NavMeshAgent _navMeshAgent;

    private AIState _aiState = AIState.Idle;
    private GameObject _player;
    private GameObject _hero;
    private GameObject[] _monsters;

    private float _lastCheckedDistanceTime = 0;
    private float _distanceToPlayer = 1000f;
    private float _distanceToHero = 1000f;
    private GameObject _closestMonster;
    private float _distanceToClosestMonster = 1000f;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        GameObject[] playerTaggedObjects = GameObject.FindGameObjectsWithTag("Player");
        Debug.Assert(playerTaggedObjects.Length > 0, "playerTaggedObjects.Length > 0");
        _player = playerTaggedObjects[0];

        GameObject[] heroTaggedObjects = GameObject.FindGameObjectsWithTag("Hero");
        Debug.Assert(heroTaggedObjects.Length > 0, "heroTaggedObjects.Length > 0");
        _hero = heroTaggedObjects[0];

        _monsters = GameObject.FindGameObjectsWithTag("Monster");
    }

    void Update()
    {
        CalculateDistances();

        Think();
    }

    private void CalculateDistances()
    {
        if (_lastCheckedDistanceTime + DistanceCheckTime < Time.time)
        {
            bool shouldCheckHero = AIType == AIType.Monster && _hero != null;
            bool shouldCheckPlayer = (AIType == AIType.Monster || AIType == AIType.Hero) && _player != null;
            bool shouldCheckMonsters = AIType == AIType.Hero && _monsters.Length > 0;

            if (shouldCheckHero)
            {
                var path = new NavMeshPath();
                if (_navMeshAgent.CalculatePath(_hero.transform.position, path))
                {
                    _distanceToHero = GetPathLength(path);
                }
            }

            if (shouldCheckPlayer)
            {
                var path = new NavMeshPath();
                if (_navMeshAgent.CalculatePath(_player.transform.position, path))
                {
                    _distanceToPlayer = GetPathLength(path);
                }
            }

            if (shouldCheckMonsters)
            {
                foreach (GameObject monster in _monsters)
                {
                    if (monster != null)
                    {
                        var path = new NavMeshPath();
                        if (_navMeshAgent.CalculatePath(_player.transform.position, path))
                        {
                            float distanceToMonster = GetPathLength(path);
                            if (distanceToMonster < _distanceToClosestMonster)
                            {
                                _distanceToClosestMonster = distanceToMonster;
                                _closestMonster = monster;
                            }
                        }
                    }
                }
            }

            _lastCheckedDistanceTime = Time.time;
        }
    }

    private static float GetPathLength(NavMeshPath path)
    {
        float length = 0.0f;

        if (path.status != NavMeshPathStatus.PathInvalid && path.corners.Length > 1)
        {
            for (int i = 1; i < path.corners.Length; i++)
            {
                length += Vector3.Distance(path.corners[i - 1], path.corners[i]);
            }
        }

        return length;
    }

    private void Think()
    {
        switch (_aiState)
        {
            case AIState.Idle:
                IdleStateThink();
                break;
            case AIState.Moving:
                MovingStateThink();
                break;
            case AIState.TargetingHero:
                TargetingHeroStateThink();
                break;
            case AIState.TargetingPlayer:
                TargetingPlayerStateThink();
                break;
            case AIState.TargetingMonster:
                TargetingMonsterStateThink();
                break;
        }
    }

    private void IdleStateThink()
    {
        // Can we target the hero
        if (_hero != null && _distanceToHero < DistanceToTargetHero)
        {
            _aiState = AIState.TargetingHero;
        }
        // Can we attack the player
        else if (_player != null && _distanceToPlayer < DistanceToTargetPlayer)
        {
            _aiState = AIState.TargetingPlayer;
        }
        else if (_closestMonster != null && _distanceToClosestMonster < DistanceToTargetMonster)
        {
            _aiState = AIState.TargetingMonster;
        }
        // 10% chance to randomly move
        else if (Random.value > .9f)
        {
            _navMeshAgent.destination = GetRandomPointOnNavMesh(this.transform.position, MaxRandomMoveDistance);
        }
    }

    private static Vector3 GetRandomPointOnNavMesh(Vector3 center, float maxDistance)
    {
        Vector2 randomPos = Random.insideUnitCircle * maxDistance + new Vector2(center.x, center.y);

        NavMeshHit hit;
        
        NavMesh.SamplePosition(randomPos, out hit, maxDistance, NavMesh.AllAreas);

        return hit.position;
    }

    private void MovingStateThink()
    {
        if (_hero != null && _distanceToHero < DistanceToTargetHero)
        {
            _aiState = AIState.TargetingHero;
        }
        // Can we attack the player
        else if (_player != null && _distanceToPlayer < DistanceToTargetPlayer)
        {
            _aiState = AIState.TargetingPlayer;
        }
    }

    private void TargetingHeroStateThink()
    {
        if (_hero == null)
        {
            _aiState = AIState.Idle;
        }
        else
        {
            _navMeshAgent.destination = _hero.transform.position;
        }
    }

    private void TargetingPlayerStateThink()
    {
        if (_player == null)
        {
            _aiState = AIState.Idle;
        }
        else
        {
            _navMeshAgent.destination = _player.transform.position;
        }
    }

    private void TargetingMonsterStateThink()
    {
        if (_closestMonster == null)
        {
            _aiState = AIState.Idle;
        }
        else
        {
            _navMeshAgent.destination = _closestMonster.transform.position;
        }
    }
    
}
