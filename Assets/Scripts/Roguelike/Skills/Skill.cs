using UnityEngine;

public abstract class Skill : ScriptableObject
{
    protected GameObject owner;

    
    public abstract Skill Clone(GameObject owner);
}

public abstract class ActiveSkill : Skill
{
    public abstract void OnUse();
}

public abstract class PassiveSkill : Skill
{
    public abstract void OnUse();
    public abstract void OnUpdate();
}
