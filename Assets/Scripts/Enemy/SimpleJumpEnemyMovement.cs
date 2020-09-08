using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleJumpEnemyMovement : SimpleWalkEnemyMovement
{
    [Header("Jump Configuration")]
    [SerializeField] private float JumpForce;
    [SerializeField] private float TimeSinceLastJump = 0;

    [SerializeField] private bool safetySet;

    private void Start()
    {
        safetySet = isSafe;
    }

    public override void DoMovement(float deltaTime)
    {
        TimeSinceLastJump += deltaTime;
        if ((Random.Range(0, 10) + TimeSinceLastJump) > 12)
        {
            TimeSinceLastJump = 0;
            Rigidbody2D.AddForce(Vector2.up * JumpForce);
            isSafe = false;
        }

        base.DoMovement(deltaTime);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            if (Rigidbody2D.velocity.y < 0 && collision.contacts[0].normal == Vector2.up)
            {
                isSafe = safetySet;
            }
        }

        base.OnCollisionEnter2D(collision);
    }
}
