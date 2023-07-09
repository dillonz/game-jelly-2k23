using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
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
                lastUseTimeSkill1 = 0;
            }

            if (skill2 != null && (lastUseTimeSkill2 + skill2.CooldownTime < Time.time) && Input.GetButtonDown("Fire3"))
            {
                skill2.OnUse();
                lastUseTimeSkill2 = 0;
            }
        }

        foreach (PassiveSkill skill in passiveSkills)
        {
            skill.OnUpdate();
        }
    }
    
    public ActiveSkill GetSkill1() { return skill1; }
    public ActiveSkill GetSkill2() { return skill2; }


    public void ChangeAlpha(float alpha, float timeLength)
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            float oldAlpha = spriteRenderer.color.a;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
            StartCoroutine(ReturnAlpha(oldAlpha, timeLength));
        }
    }

    public IEnumerator ReturnAlpha(float oldAlpha, float timeLength)
    {
        yield return new WaitForSeconds(timeLength);
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, oldAlpha);
        }
    }
}
