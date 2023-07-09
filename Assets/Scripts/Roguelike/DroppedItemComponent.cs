using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItemComponent : MonoBehaviour
{
    public Skill SkillGiven;

    private SkillComponent playerTryingToSwitch;

    private bool acceptingInput = false;

    void Start()
    {
        Debug.Assert(SkillGiven != null);
    }

    void Update()
    {
        if (acceptingInput)
        {
            // This only runs when pickup has been attempted on this object with an active skill
            if (Input.GetButtonDown("Fire2"))
            {
                playerTryingToSwitch.AddActiveSkill(0, (ActiveSkill)SkillGiven);
                playerTryingToSwitch.BlockSkillUsage = false;
                acceptingInput = false;
                Destroy(gameObject);
            }
            else if (Input.GetButtonDown("Fire3"))
            {
                playerTryingToSwitch.AddActiveSkill(1, (ActiveSkill)SkillGiven);
                playerTryingToSwitch.BlockSkillUsage = false;
                acceptingInput = false;
                Destroy(gameObject);
            }
        }
    }

    public void OnPickedUpAttempted(GameObject pickerUpper)
    {
        var skillComponent = pickerUpper.GetComponent<SkillComponent>();
        if (skillComponent != null)
        {
            if (SkillGiven is PassiveSkill passiveSkill)
            {
                skillComponent.AddPassiveSkill(passiveSkill);
                Destroy(gameObject);
            }
            else if (SkillGiven is ActiveSkill activeSkill)
            {
                uint skillSlotOpen = skillComponent.GetOpenActiveSkillIndex();
                if (skillSlotOpen < 2)
                {
                    skillComponent.AddActiveSkill(skillSlotOpen, activeSkill);
                    Destroy(gameObject);
                }
                else
                {
                    playerTryingToSwitch = skillComponent;
                    skillComponent.BlockSkillUsage = true;
                    acceptingInput = true;
                }
            }
        }
    }
}
