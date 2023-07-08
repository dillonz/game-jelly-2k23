using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof (SpriteRenderer))]
public class Fade : MonoBehaviour
{
    public float FadeStart;
    public float Duration;

    private float time = 0f;
    private new SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > FadeStart)
        {
            StartCoroutine(FadeColor(renderer.material.color, new Color(
                    renderer.material.color.r,
                    renderer.material.color.g,
                    renderer.material.color.b,
                    0
                ), Duration));
        }

        if (time > FadeStart + Duration)
        {
            Destroy(gameObject);
        }


    }

    IEnumerator FadeColor(Color start, Color end, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            //right here, you can now use normalizedTime as the third parameter in any Lerp from start to end
            renderer.material.color = Color.Lerp(start, end, normalizedTime);
            yield return null;
        }
        renderer.material.color = end; //without this, the value will end at something like 0.9992367
    }
}
