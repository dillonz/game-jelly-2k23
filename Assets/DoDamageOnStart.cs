using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DoDamageOnStart : MonoBehaviour
{
    public StatsComponent Stats;

    Collider2D coll;
    bool collided;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
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
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
