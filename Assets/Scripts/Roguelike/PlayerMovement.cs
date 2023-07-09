using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer), typeof(StatsComponent), typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float XMove { get; private set; } = 0;
    public float YMove { get; private set; } = 0;
    public Vector2 NormalDirection { get; private set; } = new Vector2(0f, 0f);
    public bool FacingLeft { get; private set; } = true;

    private StatsComponent stats;
    private Rigidbody2D rigidBody2D;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        stats = GetComponent<StatsComponent>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        XMove = Input.GetAxisRaw("Horizontal") * stats.GetSpeed();
        YMove = Input.GetAxisRaw("Vertical") * stats.GetSpeed();

        if (XMove != 0 || YMove != 0) NormalDirection = new Vector2(XMove, YMove).normalized;

        rigidBody2D.velocity = new Vector2(XMove, YMove);

        if (XMove != 0)
        {
            FacingLeft = XMove > 0;
            spriteRenderer.flipX = FacingLeft;
        }
    }

    public void StopMoving()
    {
        XMove = 0; YMove = 0;

        rigidBody2D.velocity = new Vector2(0, 0);
    }
}
