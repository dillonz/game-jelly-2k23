using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer), typeof(StatsComponent))]
public class PlayerMovement : MonoBehaviour
{
    public Animator Animator;

    public float XMove { get; private set; } = 0;
    public float YMove { get; private set; } = 0;
    public Vector2 NormalDirection { get; private set; } = new Vector2(0f, 0f);
    public bool FacingLeft { get; private set; } = true;

    private StatsComponent stats;

    private void Start()
    {
        stats = GetComponent<StatsComponent>();
    }
    void Update()
    {
        XMove = Input.GetAxisRaw("Horizontal") * Time.deltaTime * stats.GetSpeed();
        YMove = Input.GetAxisRaw("Vertical") * Time.deltaTime * stats.GetSpeed();

        NormalDirection = new Vector2(XMove, YMove).normalized;

        transform.position += new Vector3(XMove, YMove, 0);

        if (XMove != 0)
        {
            FacingLeft = XMove > 0;
            GetComponent<SpriteRenderer>().flipX = FacingLeft;
        }
    }
}
