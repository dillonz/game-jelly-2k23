using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class StatsComponent : MonoBehaviour
{
    public float StartingMaxHealth;
    public float StartingDefense;
    public float StartingAttack;
    public float StartingRange;
    public float StartingSpeed;
    public float StartingAttackCooldown;
    public float StartingCooldownReduction;

    protected float maxHealth;
    protected float defense;
    protected float attack;
    protected float range;
    protected float speed;
    protected float attackCooldown;
    protected float cooldownReduction;

    protected float currentHealth;

    protected void Start()
    {
        maxHealth = StartingMaxHealth;
        defense = StartingDefense;
        attack = StartingAttack;
        range = StartingRange;
        speed = StartingSpeed;
        attackCooldown = StartingAttackCooldown;
        cooldownReduction = StartingCooldownReduction;

        currentHealth = maxHealth;
    }

    public float GetMaxHealth() { return maxHealth; }
    public float GetDefense() {  return defense; }
    public float GetAttack() { return attack; }
    public float GetRange() { return range; }  
    public float GetSpeed() { return speed; }
    public float GetAttackCooldown() { return attackCooldown; }
    public float GetCooldownReduction() { return cooldownReduction; }

    public void OnDamage(float dealtDamage)
    {
        currentHealth -= dealtDamage * (1 - defense / 100);

        // Death
        if (currentHealth <= 0)
        {
            bool isPlayer = GetComponent<PlayerStatsComponent>() != null;
            // Do death callbacks
            var adjustStatsBehavior = GetComponent<AdjustStatsBehavior>();
            if (adjustStatsBehavior != null)
            {
                adjustStatsBehavior.AdjustPlayerStats();
            }

            var dropItemsComponent = GetComponent<DropItemsComponent>();
            if (dropItemsComponent != null)
            {
                dropItemsComponent.OnDeath();
            }

            if (!isPlayer)
            {
                Destroy(gameObject);
            }
        }
    }
}
