
using UnityEngine;

public class DropItemsComponent : MonoBehaviour
{
    public GameObject ItemToDropOnDeath;

    void Start()
    {
        this.enabled = false;
    }

    public void OnDeath()
    {
        if (ItemToDropOnDeath != null)
        {
            Instantiate(ItemToDropOnDeath, transform.position, transform.rotation);
        }
    }
}