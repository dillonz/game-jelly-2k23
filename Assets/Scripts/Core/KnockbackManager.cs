using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class KnockbackManager : MonoBehaviour
{
    public float ColorDuration= 1f;
    public float KBTime = .5f;
    public float KnockbackForce = 5f;
    public Vector3 Direction;

    private Subscription<Knockback> knockbackSub;

    void Start()
    {
        knockbackSub = EventBus.Subscribe<Knockback>(_Knockback);
    }

    void _Knockback(Knockback e)
    {
        SpriteRenderer sr = e.GO.GetComponent<SpriteRenderer>();
        if (sr == null) return;

        Color startingColor = sr.color;
        Color red = Color.red;

        StartCoroutine(FadeColor(red, startingColor, ColorDuration, sr));

        Behaviour compToDisable = e.GO.GetComponent<NavMeshAgent>();
        if (compToDisable == null) compToDisable = e.GO.GetComponent<PlayerMovement>();
        if (compToDisable == null) return;

        compToDisable.enabled = false;
        e.GO.GetComponent<Rigidbody2D>().AddForce(new Vector2(e.NormalizedDirection.x, e.NormalizedDirection.y) * KnockbackForce);
        
        StartCoroutine(EnableAfterTime(compToDisable, e.GO.GetComponent<Rigidbody2D>(), KBTime));
    }

    IEnumerator FadeColor(Color start, Color end, float duration, SpriteRenderer sr)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            //right here, you can now use normalizedTime as the third parameter in any Lerp from start to end
            sr.material.color = Color.Lerp(start, end, normalizedTime);
            yield return null;
        }
        sr.material.color = end; //without this, the value will end at something like 0.9992367
    }

    IEnumerator EnableAfterTime(Behaviour compToEnable, Rigidbody2D rb, float delay)
    {
        for (float t = 0f; t < delay; t += Time.deltaTime)
        {
            yield return null;
        }
        compToEnable.enabled = true;
        rb.velocity = Vector2.zero;
    } 
}
