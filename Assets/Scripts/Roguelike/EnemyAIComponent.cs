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
    TargetingHero,
    TargetingPlayer,
    TargetingMonster,
}


[RequireComponent(typeof(NavMeshAgent), typeof(StatsComponent))]
public class EnemyAIComponent : MonoBehaviour
{
    public AIType AIType;

    public float DistanceToTargetHero = 40f;
    public float DistanceToTargetPlayer = 40f;
    public float DistanceToTargetMonster = 40f;
    public float MaxRandomMoveDistance = 10f;

    private NavMeshAgent _navMeshAgent;
    private StatsComponent _statsComponent;

    private AIState _aiState = AIState.Idle;
    private GameObject _player;
    private GameObject _hero;
    private GameObject[] _monsters;
    
    private float _distanceToPlayer = 1000f;
    private float _distanceToHero = 1000f;
    private GameObject _closestMonster;
    private float _distanceToClosestMonster = 1000f;

    void Start()
    {
        _statsComponent = GetComponent<StatsComponent>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;

        GameObject[] playerTaggedObjects = GameObject.FindGameObjectsWithTag("Player");
        Debug.Assert(playerTaggedObjects.Length > 0, "playerTaggedObjects.Length > 0");
        _player = playerTaggedObjects[0];

        GameObject[] heroTaggedObjects = GameObject.FindGameObjectsWithTag("Hero");
        Debug.Assert(heroTaggedObjects.Length > 0, "heroTaggedObjects.Length > 0");
        _hero = heroTaggedObjects[0];

        _monsters = GameObject.FindGameObjectsWithTag("Monster");

        StartCoroutine(CalculateDistances());
        StartCoroutine(ThinkCoroutine());
    }

    void Update()
    {
    }

    private IEnumerator CalculateDistances()
    {
        for (;;)
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

            yield return new WaitForSeconds(1f);
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

    private IEnumerator ThinkCoroutine()
    {
        for (;;)
        {
            switch (_aiState)
            {
                case AIState.Idle:
                    IdleStateThink();
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

            yield return new WaitForSeconds(0.1f);
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
            _navMeshAgent.speed = _statsComponent.GetSpeed();
        }
    }

    private static Vector3 GetRandomPointOnNavMesh(Vector3 center, float maxDistance)
    {
        Vector2 randomPos = Random.insideUnitCircle * maxDistance + new Vector2(center.x, center.y);

        NavMeshHit hit;
        
        NavMesh.SamplePosition(randomPos, out hit, maxDistance, NavMesh.AllAreas);

        return new Vector3(hit.position.x, hit.position.y, 0);
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
            _navMeshAgent.speed = _statsComponent.GetSpeed();
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
            _navMeshAgent.speed = _statsComponent.GetSpeed();
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
            _navMeshAgent.speed = _statsComponent.GetSpeed();
        }
    }
    
}
