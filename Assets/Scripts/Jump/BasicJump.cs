using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Motion2d))]
public class BasicJump : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Motion2d motion2D;
    [SerializeField] private new Rigidbody2D rigidbody2D;

    [Header("Gravity")]
    [SerializeField] private float fallMultiplier = 2;
    [SerializeField] private float transitionSpeed = 1;
    private float currentScale = 1;
    private float targetScale = 1;

    [Header("Jump")]
    [SerializeField] private float force = 5;
    [SerializeField] private int count = 2;
    [SerializeField] private int currentJump = 0;


    private bool isGrounded = true;
    public bool IsGrounded => isGrounded;
    public bool CanJump => isGrounded || currentJump < count;

    public event Action OnLanding;

    void Start()
    {
        if (motion2D == null) motion2D = GetComponent<Motion2d>();
        if (rigidbody2D == null) rigidbody2D = GetComponent<Rigidbody2D>();

        motion2D.OnJump += OnJump;
    }

    private void Update()
    {
        if (rigidbody2D.velocity.y != 0)
        {
            if (!Input.GetKey(KeyCode.Space) || rigidbody2D.velocity.y < 0)
            {
                targetScale = fallMultiplier;
            }
        } else
        {
            targetScale = 1;
        }

        if (isGrounded)
        {
            currentJump = 0;
            targetScale = 1;
            currentScale = 1;
        }

        currentScale = Mathf.Lerp(currentScale, targetScale, Time.deltaTime * transitionSpeed);
        rigidbody2D.gravityScale = currentScale;
    }

    public void SetGrounding(bool grounding)
    {
        if (isGrounded && !grounding)
        {
            currentJump++;
        }

        isGrounded = grounding;

        if (grounding)
        {
            OnLanding?.Invoke();
        }
    }

    private void OnJump()
    {
        if (currentJump < count)
        {
            float jumpScale = Mathf.Clamp((float)(count - currentJump) / count, 1 / count, 1);
            
            rigidbody2D.gravityScale = 1;
            if (rigidbody2D.velocity.y < 0)
            {
                rigidbody2D.velocity = rigidbody2D.velocity * new Vector2(1, 0);
            }
            rigidbody2D.AddForce(Vector2.up * force * jumpScale, ForceMode2D.Force);

            currentJump++;
            isGrounded = false;
        }
    }
}
