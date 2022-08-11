using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlatform : Platform
{
    [SerializeField] private float _jumpForce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.ForcePush(_jumpForce);
        }
    }

    public override void Accept(IPlatformVisitor visitor)
    {
        visitor.Visit(this);
    }
}
