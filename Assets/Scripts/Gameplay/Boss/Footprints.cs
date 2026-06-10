using System;
using UnityEngine;

public class Footprints : MonoBehaviour
{
    [SerializeField] private PoolData _footprintPoolData;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private Transform _footprintSpawnPoint;
    [SerializeField] private Movement _movement;

    private ObjectPool<Footprint> _footprintPool;
    private bool _leftFoot;

    private void Awake()
    {
        _playerAnimator.FootOnGround += OnFootOnGround;
        
        var parent = new GameObject("footprintPool").transform;
        
        _footprintPool = new ObjectPool<Footprint>(_footprintPoolData.Prefab, _footprintPoolData.PoolSize, parent);
    }

    private void OnDestroy()
    {
        _playerAnimator.FootOnGround -= OnFootOnGround;
    }

    private void OnFootOnGround()
    {
        Vector2 pos = _footprintSpawnPoint.position;

        if (_leftFoot)
        {
            var velocity = _movement.MoveSpeedVelocity.normalized;
            pos += new Vector2(-velocity.y, velocity.x) * 0.2f;
        }

        var footprint = _footprintPool.Get();
        footprint.transform.position = pos;
        footprint.Enable();
        
        _leftFoot = !_leftFoot;
    }
}