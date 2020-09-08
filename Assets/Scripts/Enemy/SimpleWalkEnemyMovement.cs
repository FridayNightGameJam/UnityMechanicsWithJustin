using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWalkEnemyMovement : MonoBehaviour, IEnemyMovement
{
    [Header("Configuration")]
    [SerializeField] protected Rigidbody2D Rigidbody2D;

    [Header("Configuration")]
    [SerializeField] private int Direction; // -1 left 1 right
    [SerializeField] private float Speed;

    [Header("Safety Configuration")]
    [SerializeField] protected bool isSafe = false;
    [SerializeField] private Vector2 SafetyRay;

    public virtual void DoMovement(float deltaTime)
    {
        if(isSafe)
        {
            Debug.DrawRay(Rigidbody2D.position + (SafetyRay * transform.localScale.x), Vector2.down, Color.red, 0.2f);
            RaycastHit2D rch = Physics2D.Raycast(Rigidbody2D.position + (SafetyRay * transform.localScale.x), Vector2.down, .6f, LayerMask.GetMask("Floor"));
            if (rch.collider == null)
            {
                TurnAround();
            }
        }

        Rigidbody2D.velocity = new Vector2(Direction * Speed, Rigidbody2D.velocity.y);
    }

    public void TurnAround()
    {
        Direction *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        if (collision.collider.tag.Equals("Wall") || enemy != null)
        {
            if (Mathf.Abs(Vector2.Dot(collision.contacts[0].normal, Vector2.up)) < 1)
            {
                TurnAround();
            }
        }
    }
}
