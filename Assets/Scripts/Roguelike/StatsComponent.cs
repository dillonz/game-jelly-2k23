using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class StatsComponent : MonoBehaviour
{
    public Animator Animator;

    public int StartingMaxHealth;
    public float StartingDefense;
    public float StartingAttack;
    public float StartingRange;
    public float StartingSpeed;
    public float StartingAttackSpeed;
    public float StartingCooldownReduction;

    protected int maxHealth;
    protected float defense;
    protected float attack;
    protected float range;
    protected float speed;
    protected float attackSpeed;
    protected float cooldownReduction;

    protected int currentHealth;

    void Start()
    {
        maxHealth = StartingMaxHealth;
        defense = StartingDefense;
        attack = StartingAttack;
        range = StartingRange;
        speed = StartingSpeed;
        attackSpeed = StartingAttackSpeed;
        cooldownReduction = StartingCooldownReduction;
    }

    public int GetMaxHealth() { return maxHealth; }
    public float GetDefense() {  return defense; }
    public float GetAttack() { return attack; }
    public float GetRange() { return range; }  
    public float GetSpeed() { return speed; }
    public float GetAttackSpeed() { return attackSpeed; }
    public float GetCooldownReduction() { return cooldownReduction; }
}
