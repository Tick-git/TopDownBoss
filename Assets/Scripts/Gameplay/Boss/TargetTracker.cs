using UnityEngine;

public class TargetTracker : MonoBehaviour
{
    [SerializeField] private BossSceneReferences _bossSceneReferences;

    private Transform Target => _bossSceneReferences.Player.transform;

    private Movement _movement;

    private void Awake()
    {
        _movement = Target.GetComponent<Movement>();
    }

    public bool IsRightSideOf(Vector2 position)
    {
        return (GetTargetPosition() - position).x > 0;
    }

    public Vector2 GetTargetPosition()
    {
        return Target.position;
    }

    public Vector2 GetTargetDirection()
    {
        return (Target.position - transform.position).normalized;
    }

    public Vector2 GetTargetMoveSpeedVelocity()
    {
        return _movement.MoveSpeedVelocity;
    }

    public float DistanceToTarget()
    {
        return Vector3.Distance(Target.position, transform.position);
    }

    public Vector2 GetTargetMovePrediction(float time)
    {
        return GetTargetPosition() + GetTargetMoveSpeedVelocity() * time;
    }
}