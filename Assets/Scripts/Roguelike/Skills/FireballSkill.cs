using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Fireball")]
public class FireballSkill : Skill
{
    [SerializeField]
    private GameObject prefabToSpawn;

    public override Skill Clone(GameObject owner)
    {
        FireballSkill newSkill = ScriptableObject.CreateInstance<FireballSkill>(); ;
        newSkill.prefabToSpawn = prefabToSpawn;
        newSkill.owner = owner;

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