using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerMovement : MonoBehaviour, IMovable
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;
    [Range(1, 10)] [SerializeField] private float _observeRadius;

    private SpriteRenderer _spriteRenderer;
    private Tweener _tween;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _target.position) < _observeRadius)
        {
            Move();
        }
        else
        {
            _tween = transform.DOMove(transform.position, 1).SetAutoKill(true);
            _spriteRenderer.color = Color.white;

            _tween = null;
        }
    }

    public void Move()
    {
        _spriteRenderer.color = Color.red;

        if (_tween == null)
        {
            _tween = transform.DOMove(new Vector3(_target.position.x, transform.position.y, 0), 50.0f / _speed).SetAutoKill(true);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _observeRadius);
    }
}
