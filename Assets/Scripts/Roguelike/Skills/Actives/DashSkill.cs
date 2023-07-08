using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Dash")]
public class DashSkill : ActiveSkill
{
    [SerializeField]
    private float distance;

    public override Skill Clone(GameObject owner)
    {
        DashSkill newSkill = ScriptableObject.CreateInstance<DashSkill>();
        newSkill.distance = distance;
        newSkill.owner = owner;

        return newSkill;
    }

    public override void OnUse()
    {
    }
}