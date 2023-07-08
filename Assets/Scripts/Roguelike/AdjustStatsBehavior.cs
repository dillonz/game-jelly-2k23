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
        EventBus.Publish(new AdjustStats(MaxHealthDelta, DefenseDelta, AttackDelta, RangeDelta, SpeedDelta, AttackSpeedDelta, CooldownReductionDelta));
    }
}
