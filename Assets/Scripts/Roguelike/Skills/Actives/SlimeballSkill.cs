using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Slimeball")]
public class SlimeballSkill : ActiveSkill
{
    [SerializeField]
    private GameObject slimeballToSpawn;

    public override Skill Clone(GameObject owner)
    {
        SlimeballSkill newSkill = ScriptableObject.CreateInstance<SlimeballSkill>();
        newSkill.slimeballToSpawn = slimeballToSpawn;
        newSkill.owner = owner;
        newSkill.UIImage = UIImage;
        newSkill.CooldownTime = CooldownTime;

        return newSkill;
    }

    public override bool OnUse()
    {
        Debug.Assert(slimeballToSpawn != null);
        if (slimeballToSpawn != null)
        {
            GameObject obj = Instantiate(slimeballToSpawn, owner.transform.position, owner.transform.rotation);
            var slimeBallComp = obj.GetComponent<SlimeballComponent>();
            slimeBallComp.Caster = owner;

            var rigidBody2D = owner.GetComponent<Rigidbody2D>();
            if (rigidBody2D != null)
            {
                slimeBallComp.Direction = rigidBody2D.velocity.normalized;
            }

            // If the character isn't moving, try to figure out where they are facing
            if (slimeBallComp.Direction.magnitude < 0.01)
            {
                var playerMovement = owner.GetComponent<PlayerMovement>();
                if (playerMovement != null)
                {
                    slimeBallComp.Direction = playerMovement.NormalDirection;
                }
                else
                {
                    slimeBallComp.Direction = Vector2.right;
                }
            }
        }

        return true;
    }
}