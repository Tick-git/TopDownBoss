using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable<Bullet>
{
    private bool _isFlying = false;
    public event Action<Bullet> Hit;

    private Vector2 _direction;
    private float _damage;
    private float _speed;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void StartFlight(Vector2 direction, float damage, float speed)
    {
        if (_isFlying) return;

        _isFlying = true;

        _direction = direction;
        _damage = damage;
        _speed = speed;

        StartCoroutine(DestroyBullet());
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.ApplyDamage(_damage);
            Hit?.Invoke(this);
            return;
        }

        if (other.TryGetComponent(out Wall wall))
        {
            Hit?.Invoke(this);
            return;
        }
    }

    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(5);

        _isFlying = false;
        Hit?.Invoke(this);
    }

    public void OnReturnToPool()
    {
        _isFlying = false;
        _direction = Vector2.zero;
    }

    public void OnGetFromPool()
    {
    }

    private void FixedUpdate()
    {
        if (!_isFlying) return;

        _rigidbody2D.MovePosition(_rigidbody2D.position + _direction * (_speed * Time.fixedDeltaTime));
    }
}