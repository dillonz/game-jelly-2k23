using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStatsComponent : StatsComponent
{
    private Subscription<AdjustStats> adjustStatsSub;

    new void Start()
    {
        base.Start();
        adjustStatsSub = EventBus.Subscribe<AdjustStats>(_OnAdjustStats);
    }

    void _OnAdjustStats(AdjustStats e)
    {
        maxHealth -= e.MaxHealthDelta;
        if (maxHealth < 1) { maxHealth = 1; }
        defense -= e.DefenseDelta;
        if (defense < 1) { defense = 1; }
        attack -= e.AttackDelta;
        if (attack < 1) { attack = 1; }
        attackSpeed -= e.AttackSpeedDelta;
        if (attackSpeed < 1) { attackSpeed = 1; }
        range -= e.RangeDelta;
        if (range < 1) { range = 1; }
        speed -= e.SpeedDelta;
        if (speed < 1) { speed = 1; }
        cooldownReduction -= e.CooldownReductionDelta;
        if (cooldownReduction < 0) { cooldownReduction = 0; }
    }
}
