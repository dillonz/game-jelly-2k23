using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Slimeball")]
public class SlimeballSkill : ActiveSkill
{
    [SerializeField]
    private GameObject prefabToSpawn;

    public override Skill Clone(GameObject owner)
    {
        SlimeballSkill newSkill = ScriptableObject.CreateInstance<SlimeballSkill>();
        newSkill.prefabToSpawn = prefabToSpawn;
        newSkill.owner = owner;
        newSkill.UIImage = UIImage;
        newSkill.CooldownTime = CooldownTime;

        return newSkill;
    }

    public override void OnUse()
    {
        if (prefabToSpawn != null)
        {
            Instantiate(prefabToSpawn, owner.transform.position, owner.transform.rotation);
        }
    }
}