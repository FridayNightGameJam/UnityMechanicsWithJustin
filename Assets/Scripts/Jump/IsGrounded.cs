using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    public enum ContactType
    {
        Landing,
        Falling
    }

    [Header("Components")]
    [SerializeField] private BasicJump BasicJump;
    [SerializeField] private ContactType contactType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (contactType == ContactType.Falling) return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            BasicJump.SetGrounding(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (contactType == ContactType.Landing) return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            BasicJump.SetGrounding(false);
        }
    }
}
