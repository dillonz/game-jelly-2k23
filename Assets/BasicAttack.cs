using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatsComponent), typeof(Animator), typeof(PlayerMovement))]
public class BasicAttack : MonoBehaviour
{
    public Animator Animator;
    public GameObject AttackObject;

    private StatsComponent stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<StatsComponent>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Animator.SetBool("isAttacking", true);
            Animator.SetBool("facingLeft", GetComponent<PlayerMovement>().FacingLeft);

            GameObject go = Instantiate(AttackObject, transform.position, transform.rotation);
            go.GetComponent<DoDamageOnStart>().Stats = stats;
        }
    }
}
