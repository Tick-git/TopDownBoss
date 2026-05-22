using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private PlayerMovementData _movementData;

    private BoxCollider2D _collider;
    private Rigidbody2D _rigidbody;
    private ContactFilter2D _filter;

    public float MoveSpeedMultiplier { get; private set; }
    public Vector2 MoveSpeedVelocity { get; private set; }

    public void Initialize()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();

        _filter = new ContactFilter2D()
        {
            useTriggers = false
        };

        MoveSpeedMultiplier = 1;

        ResetMoveSpeedMultiplicator();
    }

    public void Move(Vector2 direction, float deltaTime)
    {
        float speed = _movementData.MoveSpeed * MoveSpeedMultiplier;

        MovePosition(direction, speed, deltaTime);
    }

    public void Roll(Vector2 direction, float deltaTime)
    {
        MovePosition(direction, _movementData.RollSpeed, deltaTime);
    }

    public void SetMoveSpeedMultiplicator(float value)
    {
        MoveSpeedMultiplier = value;
    }

    public void ResetMoveSpeedMultiplicator()
    {
        MoveSpeedMultiplier = 1;
    }

    public void SetMoveSpeedVelocityToZero()
    {
        MoveSpeedVelocity = Vector2.zero;
    }

    private void MovePosition(Vector2 direction, float speed, float deltaTime)
    {
        CalculateVelocityMoveSpeed(direction, deltaTime);

        float distance = speed * deltaTime;

        _rigidbody.MovePosition(_rigidbody.position + GetMoveDelta(direction, distance));
    }

    private void CalculateVelocityMoveSpeed(Vector2 direction, float deltaTime)
    {
        float distance = _movementData.MoveSpeed * MoveSpeedMultiplier * deltaTime;

        MoveSpeedVelocity = GetMoveDelta(direction, distance) / deltaTime;
    }

    private Vector2 GetMoveDelta(Vector2 direction, float distance)
    {
        RaycastHit2D[] hits = new RaycastHit2D[8];
        int count = _collider.Cast(direction, _filter, hits, distance);

        for (int i = 0; i < count; i++)
        {
            var normal = hits[i].normal;

            float dot = Vector2.Dot(direction, normal);

            if (dot < 0f)
            {
                direction -= dot * normal;
            }
        }

        return direction * distance;
    }
}