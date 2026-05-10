using UnityEngine;

public class TargetTracker : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Movement _movement;

    private void Awake()
    {
        _movement = _target.GetComponent<Movement>();
    }

    public bool IsRightSideOf(Vector2 position)
    {
        return (GetTargetPosition() - position).x > 0;
    }

    public Vector2 GetTargetPosition()
    {
        return _target.position;
    }

    public Vector2 GetTargetDirection()
    {
        return (_target.position - transform.position).normalized;
    }

    public Vector2 GetTargetMoveSpeedVelocity()
    {
        return _movement.MoveSpeedVelocity;
    }

    public float DistanceToTarget()
    {
        return Vector3.Distance(_target.position, transform.position);
    }

    public Vector2 GetTargetMovePrediction(float time)
    {
        return GetTargetPosition() + GetTargetMoveSpeedVelocity() * time;
    }
}