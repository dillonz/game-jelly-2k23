using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Skills/Fireball")]
public class FireballSkill : Skill
{
    [SerializeField]
    private GameObject fireballToSpawn;

    public override Skill Clone(GameObject owner)
    {
        FireballSkill newSkill = ScriptableObject.CreateInstance<FireballSkill>(); ;
        newSkill.fireballToSpawn = fireballToSpawn;
        newSkill.owner = owner;

        return newSkill;
    }

    public override void OnUse()
    {
        if (fireballToSpawn != null)
        {
            GameObject obj = Instantiate(fireballToSpawn, owner.transform.position, owner.transform.rotation);
            var fireballComp = obj.GetComponent<FireballComponent>();
            fireballComp.Caster = owner;

            var playerMovement = owner.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                fireballComp.Direction = playerMovement.NormalDirection;
            }

            var navMeshAgent = owner.GetComponent<NavMeshAgent>();
            if (navMeshAgent != null)
            {
                fireballComp.Direction = new Vector2(navMeshAgent.velocity.x, navMeshAgent.velocity.y).normalized;
            }


        }
    }
}