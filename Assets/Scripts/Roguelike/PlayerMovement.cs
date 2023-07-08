using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer), typeof(StatsComponent), typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public Animator Animator;

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
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        XMove = Input.GetAxisRaw("Horizontal") * stats.GetSpeed();
        YMove = Input.GetAxisRaw("Vertical") * stats.GetSpeed();

        NormalDirection = new Vector2(XMove, YMove).normalized;

        this.rigidBody2D.velocity = new Vector2(XMove, YMove);

        //transform.position += new Vector3(XMove, YMove, 0);

        if (XMove != 0)
        {
            FacingLeft = XMove > 0;
            spriteRenderer.flipX = FacingLeft;
        }
    }
}
