using System;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(BoxCollider2D))]
public class FireballComponent : MonoBehaviour
{
    public float DistanceBeforeAutoDestroy;
    public float Speed;
    public float BaseDamage;

    [NonSerialized]
    public GameObject Caster;
    [NonSerialized]
    public Vector2 Direction;

    private Vector3 startLocation;

    void Start()
    {
        Debug.Assert(Caster != null);

        startLocation = this.transform.position;
    }
    
    void Update()
    {
        if (Vector3.Distance(startLocation, this.transform.position) > DistanceBeforeAutoDestroy)
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
