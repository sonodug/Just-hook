using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicBullet : Bullet
{
    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.ApplyDamage(Damage);
            gameObject.SetActive(false);
        }
    }

    public override void Move()
    {
        transform.Translate(Speed * Time.deltaTime * Direction);
    }
}
