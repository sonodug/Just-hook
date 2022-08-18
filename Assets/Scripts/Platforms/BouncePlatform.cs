using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BouncePlatform : Platform
{
    [SerializeField] private float _jumpForce;

    public event UnityAction<Player, float> ConnectionBroken;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            ConnectionBroken?.Invoke(player, _jumpForce);
        }
    }

    public override void Accept(IPlatformVisitor visitor)
    {
        visitor.Visit(this);
    }
}
