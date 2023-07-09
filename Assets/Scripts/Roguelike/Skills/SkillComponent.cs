using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillComponent : MonoBehaviour
{
    public ActiveSkill TestSkill;

    [NonSerialized]
    public bool BlockSkillUsage = false;

    private ActiveSkill skill1;
    private ActiveSkill skill2;

    private float lastUseTimeSkill1 = 0;
    private float lastUseTimeSkill2 = 0;

    private List<PassiveSkill> passiveSkills;

    void Start()
    {
        passiveSkills = new List<PassiveSkill>();

        AddActiveSkill(0, TestSkill);
    }

    public uint GetOpenActiveSkillIndex()
    {
        if (skill1 == null)
        {
            return 0;
        }

        if (skill2 == null)
        {
            return 1;
        }

        return 1000;
    }

    public void AddActiveSkill(uint pos, ActiveSkill skill)
    {
        Debug.Assert(pos < 2);

        if (pos == 0)
        {
            skill1 = (ActiveSkill)skill.Clone(this.gameObject);
            lastUseTimeSkill1 = Time.time;
        }
        else if (pos == 1)
        {
            skill2 = (ActiveSkill)skill.Clone(this.gameObject);
            lastUseTimeSkill2 = Time.time;
        }
    }

    public void AddPassiveSkill(PassiveSkill skill)
    {
        passiveSkills.Add(skill);
    }

    void Update()
    {
        if (!BlockSkillUsage)
        {
            if (skill1 != null && (lastUseTimeSkill1 + skill1.CooldownTime < Time.time) && Input.GetButtonDown("Fire2"))
            {
                skill1.OnUse();
                lastUseTimeSkill1 = Time.time;
            }

            if (skill2 != null && (lastUseTimeSkill2 + skill2.CooldownTime < Time.time) && Input.GetButtonDown("Fire3"))
            {
                skill2.OnUse();
                lastUseTimeSkill2 = Time.time;
            }
        }

        foreach (PassiveSkill skill in passiveSkills)
        {
            skill.OnUpdate();
        }
    }
}
