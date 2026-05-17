using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossTeleport : MonoBehaviour
{
    [SerializeField] private LayerMask _wallLayer;
    [SerializeField] private TeleportData _data;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void TeleportIntoOrbit(Vector2 target)
    {
        List<Vector2> teleportPositions = new List<Vector2>();

        var startRotation = (360.0f - _data.AreaAngle) / 2.0f;
        Vector2 startDir = Quaternion.Euler(0, 0, startRotation) * (_rb.position - target).normalized;

        for (int i = 0; i < _data.PositionsCount; i++)
        {
            Vector2 direction = Quaternion.Euler(0, 0, i * _data.AreaAngle / (_data.PositionsCount - 1)) * startDir;

            RaycastHit2D hit = Physics2D.Raycast(target, direction, _data.OrbitRadius, _wallLayer);

            if (!hit)
            {
                teleportPositions.Add(target + direction * _data.OrbitRadius);
            }
        }

        if (teleportPositions.Count > 0)
            _rb.MovePosition(teleportPositions[Random.Range(0, teleportPositions.Count)]);
    }
}