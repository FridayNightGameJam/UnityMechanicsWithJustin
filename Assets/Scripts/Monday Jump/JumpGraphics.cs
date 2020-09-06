using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Motion2d))]
[RequireComponent(typeof(BasicJump))]
public class JumpGraphics : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private BasicJump JumpComponent;
    [SerializeField] private Motion2d Motion2d;
    [SerializeField] private Transform Graphics;

    [Header("Squish")]
    [SerializeField] private float amount = .2f;
    [SerializeField] private float speed = 1;
    
    private float YScale = 1;

    private void Start()
    {
        if (JumpComponent == null) JumpComponent = GetComponent<BasicJump>();
        if (Motion2d == null) Motion2d = GetComponent<Motion2d>();

        JumpComponent.OnLanding += OnLanding;
        Motion2d.OnJump += OnJump;
    }

    private void Update()
    {
        if (Mathf.Abs(YScale - 1) > float.Epsilon)
        {
            YScale = Mathf.Lerp(YScale, 1, Time.deltaTime * speed);
        }
        Graphics.localPosition = new Vector3(0, (YScale - 1) / 2, 0);
        Graphics.localScale = new Vector3(1, YScale, 0);
    }

    private void OnJump()
    {
        if (JumpComponent.CanJump)
        {
            YScale = 1 + amount;
        }
    }

    private void OnLanding()
    {
        YScale = 1 - amount;
    }
}
