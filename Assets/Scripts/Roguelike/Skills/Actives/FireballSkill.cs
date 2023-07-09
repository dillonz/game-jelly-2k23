using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Skills/Fireball")]
public class FireballSkill : ActiveSkill
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

            var rigidBody2D = owner.GetComponent<Rigidbody2D>();
            if (rigidBody2D != null)
            {
                fireballComp.Direction = rigidBody2D.velocity.normalized;
            }

            // If the character isn't moving, try to figure out where they are facing
            if (fireballComp.Direction.magnitude < 0.01)
            {
                var playerMovement = owner.GetComponent<PlayerMovement>();
                if (playerMovement != null)
                {
                    fireballComp.Direction = playerMovement.NormalDirection;
                }
                else
                {
                    fireballComp.Direction = Vector2.right;
                }
            }
        }
    }
}