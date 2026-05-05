using UnityEngine;

public struct BulletFlightParams
{
    public BulletFlightParams(Vector2 startPos, Vector2 direction, float damage, float speed, float range,
        IDamageable owner)
    {
        StartPos = startPos;
        Direction = direction;
        Damage = damage;
        Speed = speed;
        Range = range;
        Owner = owner;
    }

    public readonly Vector2 StartPos;
    public readonly Vector2 Direction;
    public readonly float Damage;
    public readonly float Speed;
    public readonly float Range;
    public readonly IDamageable Owner;
}