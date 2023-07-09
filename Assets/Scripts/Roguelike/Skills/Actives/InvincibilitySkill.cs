using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Invincibility")]
public class InvincibilitySkill : ActiveSkill
{

    public override Skill Clone(GameObject owner)
    {
        InvincibilitySkill newSkill = ScriptableObject.CreateInstance<InvincibilitySkill>();
        newSkill.owner = owner;

        return newSkill;
    }

    public override void OnUse()
    {
    }
}