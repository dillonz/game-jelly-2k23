using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Invincibility")]
public class InvincibilitySkill : ActiveSkill
{
    public float InvulnerableTime = 1f;
    public float InvulnAlpha = 0.5f;

    public override Skill Clone(GameObject owner)
    {
        InvincibilitySkill newSkill = ScriptableObject.CreateInstance<InvincibilitySkill>();
        newSkill.owner = owner;
        newSkill.UIImage = UIImage;
        newSkill.CooldownTime = CooldownTime;
        newSkill.InvulnerableTime = InvulnerableTime;
        newSkill.InvulnAlpha = InvulnAlpha;

        return newSkill;
    }

    public override bool OnUse()
    {
        var statComponent = owner.GetComponent<StatsComponent>();
        Debug.Assert(statComponent != null);
        if (statComponent != null)
        {
            statComponent.SetInvulnerable(InvulnerableTime);
        }

        var skillComponent = owner.GetComponent<SkillComponent>();
        Debug.Assert(skillComponent != null);
        if (skillComponent != null)
        {
            skillComponent.ChangeAlpha(InvulnAlpha, InvulnerableTime);
        }

        return true;
    }
}