using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private float MaxHealth = 10;
    [SerializeField] private float CurrentHealth = 10;

    public void Damage(float Amount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - Amount, 0, MaxHealth);

        if (CurrentHealth <= 0)
        {
            GameManager.Instance.RestartLevel();

            Destroy(gameObject);
        }
    }
}
