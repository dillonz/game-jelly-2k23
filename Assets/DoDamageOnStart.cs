using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DoDamageOnStart : MonoBehaviour
{
    public GameObject Attacker;

    Collider2D coll;
    bool collided;
    
    public StatsComponent Stats { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        Stats = Attacker.GetComponent<StatsComponent>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collided)
        {
            StatsComponent otherStats = collision.gameObject.GetComponent<StatsComponent>();
            if (otherStats != null && otherStats != Stats)
            {
                otherStats.OnDamage(Stats.GetAttack());
                collided = true;

                EventBus.Publish(new Knockback(collision.gameObject, (collision.gameObject.transform.position - Attacker.transform.position).normalized));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
