using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(SkillComponent))]
public class PlayerPickupComponent : MonoBehaviour
{
    public float DistanceToDetect = 5f;
    public GameObject HelpText;

    private SkillComponent skillComponent;

    void Start()
    {
        skillComponent = GetComponent<SkillComponent>();
        StartCoroutine(CheckForDroppedItems());
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

    private IEnumerator CheckForDroppedItems()
    {
        for (;;)
        {
            bool found = false;
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("DroppedItems"))
            {
                if (Vector2.Distance(obj.transform.position, this.transform.position) < DistanceToDetect)
                {
                    found = true;
                    if (HelpText != null)
                    {
                        HelpText.SetActive(true);
                    }
                    break;
                }
            }

            if (!found)
            {
                if (HelpText != null)
                {   
                    HelpText.SetActive(false);
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }
}
