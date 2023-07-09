using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkillComponent))]
public class PlayerPickupComponent : MonoBehaviour
{
    public float DistanceToDetect = 5f;

    private SkillComponent skillComponent;

    void Start()
    {
        skillComponent = GetComponent<SkillComponent>();
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Use"))
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("DroppedItems"))
            {
                if (Vector2.Distance(obj.transform.position, this.transform.position) < DistanceToDetect)
                {
                    var droppedItemComponent = obj.GetComponent<DroppedItemComponent>();
                    Debug.Assert(droppedItemComponent != null);
                    droppedItemComponent.OnPickedUpAttempted(gameObject);

                    break;
                }
            }
        }
    }
}
