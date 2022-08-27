using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BouncePlatform : Platform
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            TryBreakConnection();
        }
    }

    public override void Accept(IEnvironmentVisitor visitor)
    {
        visitor.Visit(this);
    }
}
