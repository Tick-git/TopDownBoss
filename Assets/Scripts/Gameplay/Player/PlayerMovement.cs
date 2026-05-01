using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovementData _playerMovementData;
    [SerializeField] private LayerMask _wallLayer;

    private BoxCollider2D _playerCollider;
    private Rigidbody2D _rigidbody;

    public float MoveSpeedMultiplier { get; private set; }

    public void Initialize()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerCollider = GetComponent<BoxCollider2D>();
        ResetMoveSpeedMultiplicator();
    }

    public void Move(Vector2 direction, float deltaTime)
    {
        float speed = _playerMovementData.MoveSpeed * MoveSpeedMultiplier;

        MovePosition(direction, speed, deltaTime);
    }

    public void Roll(Vector2 direction, float deltaTime)
    {
        MovePosition(direction, _playerMovementData.RollSpeed, deltaTime);
    }

    public void SetMoveSpeedMultiplicator(float value)
    {
        MoveSpeedMultiplier = value;
    }

    public void ResetMoveSpeedMultiplicator()
    {
        MoveSpeedMultiplier = 1;
    }

    private void MovePosition(Vector2 direction, float speed, float deltaTime)
    {
        float distance = speed * deltaTime;

        _rigidbody.MovePosition(_rigidbody.position + GetMoveDelta(direction, distance));
    }

    private Vector2 GetMoveDelta(Vector2 direction, float distance)
    {
        RaycastHit2D[] hits = new RaycastHit2D[8];
        int count = _playerCollider.Cast(direction, hits, distance);

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