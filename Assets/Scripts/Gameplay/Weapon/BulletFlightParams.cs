using UnityEngine;

public struct BulletFlightParams
{
    public BulletFlightParams(Vector2 startPosition, Vector2 direction, float damage, float speed, float range)
    {
        StartPosition = startPosition;
        Direction = direction;
        Damage = damage;
        Speed = speed;
        Range = range;
    }

    public readonly Vector2 StartPosition;
    public readonly Vector2 Direction;
    public readonly float Damage;
    public readonly float Speed;
    public readonly float Range;
}