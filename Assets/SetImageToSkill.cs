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
        Sprite uiImage = SkillNumber == 1 ? skills.GetSkill1().UIImage : skills.GetSkill2().UIImage;
        if (img.sprite != uiImage)
        {
            img.sprite = uiImage;
            Debug.Log(img.sprite);
        }
    }
}
