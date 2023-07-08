using UnityEngine;

public abstract class Skill : ScriptableObject
{
    protected GameObject owner;

    public abstract Skill Clone(GameObject owner);

    public abstract void OnUse();
}