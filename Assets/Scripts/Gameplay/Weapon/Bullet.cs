using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable<Bullet>
{
    [SerializeField]  private float _speed;

    public event Action<Bullet> Hit;
    
    private bool _isFlying = false;
    private Vector2 _direction;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void StartFlight(Vector2 direction)
    {
        if (_isFlying) return;
        
        _isFlying = true;
        _direction = direction;

        StartCoroutine(DestroyBullet());
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

    public void OnGetFromPool() {}

    private void FixedUpdate()
    {
        if (!_isFlying) return;
        
        _rigidbody2D.MovePosition(_rigidbody2D.position + _direction * (_speed * Time.fixedDeltaTime));
    }
}