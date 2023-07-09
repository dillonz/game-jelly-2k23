using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AnimateRefillOnCooldown : MonoBehaviour
{
    public string Keybind = "Fire1";
    public int SkillNumber = 0; // 0 for normal attack

    private PlayerStatsComponent stats;
    private SkillComponent skills;
    private bool coolingDown;
    private Image cooldown;
    private float mostRecentTime;
    private float cooldownTime;

    private void Start()
    {
        cooldown = GetComponent<Image>();
        stats = transform.parent.transform.parent.GetComponent<HideUIElements>().Stats;
        skills = transform.parent.transform.parent.GetComponent<HideUIElements>().Skills;
    }

    // Update is called once per frame
    void Update()
    {
        GetCoolDownTime();
        if (Input.GetButtonDown(Keybind) && !coolingDown)
        {
            coolingDown = true ;
            mostRecentTime = Time.time;
        }

        if (coolingDown && ((Time.time - mostRecentTime) / cooldownTime >= 1f))
        {
            coolingDown = false ;
            cooldown.fillAmount = 1f;
        }
        else if (coolingDown)
        {
            cooldown.fillAmount = (Time.time - mostRecentTime) / cooldownTime;
        }
    }

    protected void GetCoolDownTime()
    {
        switch (SkillNumber)
        {
            case 1:
                cooldownTime = skills.GetSkill1().CooldownTime;
                break;
            case 2:
                cooldownTime = skills.GetSkill2().CooldownTime;
                break;
            default:
                cooldownTime = stats.GetAttackCooldown();
                break;
        }
    }
}
