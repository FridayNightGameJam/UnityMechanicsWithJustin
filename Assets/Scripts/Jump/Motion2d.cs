using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion2d : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D Rigidbody2D;

    [Header("Configuration")]
    [SerializeField] private float speed = 5;
    [SerializeField] private float accellerationTime = 3;

    public event Action OnJump;

    private float currentSpeed = 0;

    private void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            currentSpeed = Mathf.Lerp(currentSpeed, -speed, Time.deltaTime * accellerationTime);
        } else if (Input.GetKey(KeyCode.D))
        {
            currentSpeed = Mathf.Lerp(currentSpeed, speed, Time.deltaTime * accellerationTime);
        } else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime * accellerationTime * 3);
        }

        Rigidbody2D.velocity = new Vector2(currentSpeed, Rigidbody2D.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJump?.Invoke();
        }
    }
}
