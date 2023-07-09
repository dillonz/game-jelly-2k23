using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SetImageToSkill : MonoBehaviour
{
    public int SkillNumber = 1;

    private SkillComponent skills;
    private Image img;

    // Start is called before the first frame update
    void Start()
    {
        skills = transform.parent.transform.parent.GetComponent<HideUIElements>().Skills;
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Sprite uiImage = null;
        if (SkillNumber == 1 && skills.GetSkill1() != null)
        {
            uiImage = skills.GetSkill1().UIImage;
        }
        else if (SkillNumber == 2 && skills.GetSkill2() != null)
        {
            uiImage = skills.GetSkill2().UIImage;
        }
        if (uiImage != null && img.sprite != uiImage)
        {
            img.sprite = uiImage;
            Debug.Log(img.sprite);
        }
    }
}
