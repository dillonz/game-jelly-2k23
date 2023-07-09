using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(StatsComponent), typeof(Animator), typeof(NavMeshAgent))]
public class EnemyAttack : MonoBehaviour
{
    public GameObject AttackObject;

    StatsComponent stats;
    NavMeshAgent navMeshAgent;
    float mostRecentAttackTime;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<StatsComponent>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        this.enabled = false;
    }

    void Update()
    {
    }

    public void TryAttack()
    {
        if (mostRecentAttackTime + stats.GetAttackCooldown() < Time.time)
        {
            Vector2 dir = navMeshAgent.destination - transform.position;
            GameObject go = Instantiate(AttackObject, transform.position + (Vector3)(dir.normalized * stats.GetRange() / 2 + dir.normalized / 4), transform.rotation);
            go.GetComponent<DoDamageOnStart>().Attacker = gameObject;
            go.transform.localScale = Vector3.one * stats.GetRange();
            mostRecentAttackTime = Time.time;
        }
    }
}