using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatsComponent), typeof(Animator), typeof(PlayerMovement))]
public class BasicAttack : MonoBehaviour
{
    public float FreezeTime = 0.25f;
    public Animator Animator;
    public GameObject AttackObject;

    StatsComponent stats;
    PlayerMovement playerMovement;
    float mostRecentAttackTime;
    bool canAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<StatsComponent>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            Animator.SetBool("isAttacking", true);
            Animator.SetBool("facingLeft", playerMovement.FacingLeft);

            canAttack = false;

            GameObject go = Instantiate(AttackObject, transform.position + (Vector3)playerMovement.NormalDirection, transform.rotation);
            go.GetComponent<DoDamageOnStart>().Stats = stats;
            if (playerMovement.FacingLeft) { go.GetComponent<SpriteRenderer>().flipX = true; };
            playerMovement.StopMoving();
            playerMovement.enabled = false;
            mostRecentAttackTime = Time.time;
        }

        if (!playerMovement.enabled && Time.time - mostRecentAttackTime > FreezeTime)
        {
            playerMovement.enabled = true;
        }

        if (!canAttack && Time.time - mostRecentAttackTime > stats.GetAttackCooldown())
        {
            canAttack = true;
        }
    }
}
