using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePulse : MonoBehaviour
{
    public float amplitude;
    public float period;

    Vector3 initialSize;

    // Start is called before the first frame update
    void Start()
    {
        initialSize = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = initialSize * (1 + amplitude * Mathf.Sin(Time.time / period));
    }
}