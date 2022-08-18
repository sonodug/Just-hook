using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private Hook _hook;
    [SerializeField] private Transform _hookPivot;
    [SerializeField] private float _health;
    [SerializeField] private float _hookDamage;
    [SerializeField] private int _priceLevelCount;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        Instantiate(_hook, _hookPivot);
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Obstacle>(out Obstacle obstacle))
        {
            Destroy(gameObject);
        }

    }

    public void ForcePush(float jumpForce, Vector3 direction)
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
    }
}
