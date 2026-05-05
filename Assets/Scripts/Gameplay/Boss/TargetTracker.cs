using UnityEngine;

public class TargetTracker : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Vector3 _lastTargetPosition;

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

    public Vector2 GetTargetMoveDirection()
    {
        return (_target.position - _lastTargetPosition).normalized;
    }

    public Vector2 GetNextPositionPrediction()
    {
        return GetTargetPosition() + GetTargetMoveDirection();
    }

    public float DistanceToTarget()
    {
        return Vector3.Distance(_target.position, transform.position);
    }

    private void LateUpdate()
    {
        _lastTargetPosition = GetTargetPosition();
    }
}