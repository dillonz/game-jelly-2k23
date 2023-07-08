using UnityEngine;

public class SkillComponent : MonoBehaviour
{
    public Skill TestSkill;

    private Skill skill1;
    private Skill skill2;

    void Start()
    {
        AddSkill(0, TestSkill);
    }

    void AddSkill(uint pos, Skill skill)
    {
        Debug.Assert(pos < 2);

        if (pos == 0)
        {
            skill1 = skill.Clone(this.gameObject);
        }
        else if (pos == 1)
        {
            skill2 = skill.Clone(this.gameObject);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            skill1.OnUse();
        }
    }
}
