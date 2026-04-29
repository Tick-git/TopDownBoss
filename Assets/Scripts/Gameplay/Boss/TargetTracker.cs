using UnityEngine;

public class TargetTracker : MonoBehaviour
{
    [SerializeField] private Transform _target;

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
}