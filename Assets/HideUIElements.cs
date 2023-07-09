using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideUIElements : MonoBehaviour
{
    public GameObject Skill1UI;
    public GameObject Skill2UI;
    public SkillComponent Skills;
    public PlayerStatsComponent Stats;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Skill1UI.SetActive(Skills.GetSkill1() != null);
        Skill2UI.SetActive(Skills.GetSkill2() != null);
    }
}
