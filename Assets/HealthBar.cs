using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HealthBar : MonoBehaviour
{
    private StatsComponent stats;
    private Image img;

    // Start is called before the first frame update
    void Start()
    {
        stats = transform.parent.transform.parent.GetComponent<HideUIElements>().Stats;
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        img.fillAmount = stats.GetCurrentHealth() / stats.GetMaxHealth();
    }
}
