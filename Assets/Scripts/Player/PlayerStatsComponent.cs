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
        if (defense < 0) { defense = 0; }
        else if (defense > 99) { defense = 99; }
        attack -= e.AttackDelta;
        if (attack < 1) { attack = 1; }
        attackCooldown -= e.AttackSpeedDelta;
        if (attackCooldown < 0) { attackCooldown = 0; }
        range -= e.RangeDelta;
        if (range < .5f) { range = .5f; }
        speed -= e.SpeedDelta;
        if (speed < 1) { speed = 1; }
        cooldownReduction -= e.CooldownReductionDelta;
        if (cooldownReduction < 0) { cooldownReduction = 0; }
        else if (cooldownReduction > 99) { cooldownReduction = 99; }
    }
}
