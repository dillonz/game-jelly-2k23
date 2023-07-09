using System.Reflection;
using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Dash")]
public class DashSkill : ActiveSkill
{
    [SerializeField]
    private float force;

    [SerializeField]
    private float time;

    public override Skill Clone(GameObject owner)
    {
        DashSkill newSkill = ScriptableObject.CreateInstance<DashSkill>();
        newSkill.force = force;
        newSkill.time = time;
        newSkill.owner = owner;
        newSkill.UIImage = UIImage;
        newSkill.CooldownTime = CooldownTime;

        return newSkill;
    }

    public override bool OnUse()
    {
        var playerMovement = owner.GetComponent<PlayerMovement>();
        if (playerMovement != null && playerMovement.enabled)
        {
            playerMovement.DoDash(force, time);
            return true;
        }
        else
        {
            return false;
        }
    }
}