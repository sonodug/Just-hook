using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SelfGuidedBullet : Bullet
{
    private void Start()
    {
        Vector3 rotation = new Vector3(transform.rotation.eulerAngles.x, 0, 0);
        //transform.rotation = Quaternion.Euler(rotation);

        //fix rotation
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
        }

        gameObject.SetActive(false);
    }

    public override void Move()
    {
        transform.Translate(Speed * Time.deltaTime * Vector3.forward);
    }
}
