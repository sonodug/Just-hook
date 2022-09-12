using Entities.Player;
using UnityEngine;

public class BasicBullet : global::Bullet
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
