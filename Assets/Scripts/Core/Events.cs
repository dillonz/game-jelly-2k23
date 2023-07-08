/*
 *  Pub Sub Event Class Definitions
 *   - Include a "Usage" comment for each event.
 */

using UnityEngine;

// Template for a class to use
// public class SwitchTile
// {
//     // Usage: Notifies tiles of a particular color to "switch" (on/off)
//     public Color color;
//     public int puzzleId;
//     public bool isLit;
//
//     public SwitchTile(Color _color, int _puzzleId, bool _isLit)
//     {
//         color = _color;
//         puzzleId = _puzzleId;
//         isLit = _isLit;
//     }
// }
// In Start(), add the subscription like so:
// switchTileSub = EventBus.Subscribe<SwitchTile>(_OnSwitchTile);
//
// Then create the function like so:
// private void _OnSwitchTile(SwitchTile e) {}
public class AdjustStats
{
    // Usage: Changes the players stats accordingly
    public int MaxHealthDelta;
    public float DefenseDelta;
    public float AttackDelta;
    public float RangeDelta;
    public float SpeedDelta;
    public float AttackSpeedDelta;
    public float CooldownReductionDelta;

    public AdjustStats(int maxHealthDelta, float defenseDelta, float attackDelta, float rangeDelta, float speedDelta, float attackSpeedDelta, float cooldownReductionDelta)
    {
        MaxHealthDelta = maxHealthDelta;
        DefenseDelta = defenseDelta;
        AttackDelta = attackDelta;
        RangeDelta = rangeDelta;
        SpeedDelta = speedDelta;
        AttackSpeedDelta = attackSpeedDelta;
        CooldownReductionDelta = cooldownReductionDelta;
    }
}