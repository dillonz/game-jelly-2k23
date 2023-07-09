using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SkillComponent : MonoBehaviour
{
    private ActiveSkill skill1;
    private ActiveSkill skill2;

    private float lastUseTimeSkill1 = 0;
    private float lastUseTimeSkill2 = 0;

    private uint oldestUpdatedSkill = 0;

    private List<PassiveSkill> passiveSkills;

    void Start()
    {
        passiveSkills = new List<PassiveSkill>();
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

    public void UpdateActiveSkill(ActiveSkill skill)
    { 
        if (oldestUpdatedSkill == 0)
        {
            skill1 = (ActiveSkill)skill.Clone(this.gameObject);
            lastUseTimeSkill1 = 0;
            oldestUpdatedSkill = 1;
        }
        else if (oldestUpdatedSkill == 1)
        {
            skill2 = (ActiveSkill)skill.Clone(this.gameObject);
            lastUseTimeSkill2 = 0;
            oldestUpdatedSkill = 0;
        }
    }

    public void AddPassiveSkill(PassiveSkill skill)
    {
        passiveSkills.Add(skill);
    }

    void Update()
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
