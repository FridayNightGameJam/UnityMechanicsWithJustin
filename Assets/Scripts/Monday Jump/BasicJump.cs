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

    [Header("Configuration")]
    [SerializeField] private float DropScale = 2;
    [SerializeField] private float JumpHeight = 5;
    [SerializeField] private int JumpNumber = 2;
    [SerializeField] private int currentJump = 0;
    [SerializeField] private bool isGrounded = true;

    public bool IsGrounded => isGrounded;

    public event Action OnLanding;

    // Start is called before the first frame update
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
                rigidbody2D.gravityScale = DropScale;
            }
        } else
        {
            rigidbody2D.gravityScale = 1;
        }

        if (isGrounded)
        {
            currentJump = 0;
        }
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
        if (currentJump < JumpNumber)
        {
            float jumpScale = Mathf.Clamp((float)(JumpNumber - currentJump) / JumpNumber, 1 / JumpNumber, 1);
            
            rigidbody2D.gravityScale = 1;
            if (rigidbody2D.velocity.y < 0)
            {
                rigidbody2D.velocity = rigidbody2D.velocity * new Vector2(1, 0);
            }
            rigidbody2D.AddForce(Vector2.up * JumpHeight * jumpScale, ForceMode2D.Force);

            currentJump++;
            isGrounded = false;
        }
    }
}
