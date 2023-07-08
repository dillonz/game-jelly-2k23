using System;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(BoxCollider2D))]
public class FireballComponent : MonoBehaviour
{
    public float TimeBeforeDestroy;
    public float Speed;
    public float BaseDamage;

    [NonSerialized]
    public GameObject Caster;
    [NonSerialized]
    public Vector2 Direction;

    private float timeElapsed = 0;

    void Start()
    {
        Debug.Assert(Caster != null);
    }
    
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > TimeBeforeDestroy)
        {
            Destroy(this.gameObject);
        }

        this.transform.position += new Vector3(Direction.x, Direction.y, 0) * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == this.Caster)
        {
            return;
        }

        StatsComponent casterStats = Caster.GetComponent<StatsComponent>();
        StatsComponent otherStats = other.GetComponent<StatsComponent>();

        if (casterStats != null && otherStats != null)
        {
            otherStats.OnDamage(BaseDamage + casterStats.GetAttack());
        }

        Destroy(this.gameObject);
    }
}
