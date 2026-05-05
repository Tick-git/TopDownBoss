using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable<Bullet>
{
    private bool _isFlying = false;
    public event Action<Bullet> FlightEnded;

    private BulletFlightParams _bulletParams;

    private Rigidbody2D _rigidbody2D;

    private static readonly Vector2 PooledPosition = new Vector2(100, 0);

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void StartFlight(BulletFlightParams bulletFlightParams)
    {
        if (_isFlying) return;

        _isFlying = true;
        _bulletParams = bulletFlightParams;
        _rigidbody2D.position = _bulletParams.StartPos;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            if (damageable == _bulletParams.Owner) return;

            damageable.ApplyDamage(_bulletParams.Damage);
            EndFlight();
            return;
        }

        if (other.TryGetComponent(out Wall wall))
        {
            EndFlight();
            return;
        }
    }

    private void EndFlight()
    {
        _isFlying = false;
        FlightEnded?.Invoke(this);
    }

    public void OnReturnToPool()
    {
        _isFlying = false;
        transform.position = PooledPosition;
    }

    public void OnGetFromPool()
    {
    }

    private void FixedUpdate()
    {
        if (!_isFlying) return;

        Vector2 moveDelta = _bulletParams.Direction * (_bulletParams.Speed * Time.fixedDeltaTime);

        _rigidbody2D.MovePosition(_rigidbody2D.position + moveDelta);

        if (Vector2.Distance(_bulletParams.StartPos, _rigidbody2D.position) >= _bulletParams.Range)
        {
            EndFlight();
        }
    }
}