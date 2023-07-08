using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CombatComponent : MonoBehaviour
{
    public Animator Animator;
    
    public uint Health;
    public uint MaxHealth;

    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Animator.SetBool("isAttacking", true);
        }
    }
}
