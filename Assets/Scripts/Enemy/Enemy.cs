using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private IEnemyMovement enemyMovement;
    [SerializeField] private Transform Graphics;
    [SerializeField] private Rigidbody2D Rigidbody2D;
    [SerializeField] private Collider2D Collider2D;

    [Header("Configuration")]
    [SerializeField] private float Damage = 10;
    [SerializeField] private bool IsDead = false;
    [SerializeField] private bool KillableFromJump = false;

    [Header("Death Configuration")]
    [SerializeField] private float DeathHeight = 1;
    [SerializeField] private float DeathPosition = 0.06f;

    private void Start()
    {
        enemyMovement = GetComponent<IEnemyMovement>();
    }

    private void Update()
    {
        if (enemyMovement != null && !IsDead) enemyMovement.DoMovement(Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Player"))
        {
            // Damage Player
            Player player = collision.collider.GetComponent<Player>();
            if (player != null)
            {
                if (KillableFromJump)
                {
                    float theta = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x);
                    if (.8 <= theta && theta <= 2.2f)
                    {
                        // player killed enemy
                        IsDead = true;
                        StartCoroutine(DeathAnimation());
                        return;
                    }
                }

                player.Damage(Damage);
            }
        }
    }

    private IEnumerator DeathAnimation()
    {
        Collider2D.enabled = false;
        Rigidbody2D.velocity = Vector2.zero;
        Rigidbody2D.isKinematic = true;

        Graphics.localScale = new Vector3(Graphics.localScale.x, DeathHeight, 1);
        Graphics.transform.localPosition = new Vector3(Graphics.transform.localPosition.x, DeathPosition, 0);

        // Squash Graphics
        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }
}
