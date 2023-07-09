using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustStatsBehavior : MonoBehaviour
{
    public int MaxHealthDelta;
    public float DefenseDelta;
    public float AttackDelta;
    public float RangeDelta;
    public float SpeedDelta;
    public float AttackSpeedDelta;
    public float CooldownReductionDelta;

    public void AdjustPlayerStats()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        if (go != null)
        {
            var statsComponent = go.GetComponent<StatsComponent>();
            if (statsComponent != null)
            {
                statsComponent.AdjustStats(new AdjustStats(MaxHealthDelta, DefenseDelta, AttackDelta, RangeDelta,
                    SpeedDelta, AttackSpeedDelta, CooldownReductionDelta));
            }
        }
    }
}
