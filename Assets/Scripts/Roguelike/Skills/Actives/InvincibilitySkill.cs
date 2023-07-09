using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Invincibility")]
public class InvincibilitySkill : ActiveSkill
{

    public override Skill Clone(GameObject owner)
    {
        InvincibilitySkill newSkill = ScriptableObject.CreateInstance<InvincibilitySkill>();
        newSkill.owner = owner;
        newSkill.UIImage = UIImage;
        newSkill.CooldownTime = CooldownTime;

        return newSkill;
    }

    public override void OnUse()
    {
    }
}