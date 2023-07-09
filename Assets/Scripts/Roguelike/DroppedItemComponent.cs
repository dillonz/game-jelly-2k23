using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItemComponent : MonoBehaviour
{
    public Skill SkillGiven;

    void Start()
    {
        Debug.Assert(SkillGiven != null);
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
                skillComponent.UpdateActiveSkill(activeSkill);
                Destroy(gameObject);
            }
        }
    }
}
