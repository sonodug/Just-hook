using Entities.Player;
using UnityEngine;
using Zenject;

public class SelfGuidedBullet : Bullet
{
    [Inject] private Player target;

    private void Start()
    {
        Direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
    }

    public override void Move()
    {
        transform.Translate(Speed * Time.deltaTime * Vector2.right);
    }
}
