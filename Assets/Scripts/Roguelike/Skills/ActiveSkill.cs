
using UnityEngine;

public abstract class ActiveSkill : Skill
{
    public float CooldownTime;
    public Sprite UIImage;

    public abstract void OnUse();
}