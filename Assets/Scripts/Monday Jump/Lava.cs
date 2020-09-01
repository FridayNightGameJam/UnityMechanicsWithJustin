using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject LossText;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            LossText.SetActive(true);
        }
    }
}
