using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private BossVisuals _visuals;
    
    private TargetTracker _targetTracker;

    private void Awake()
    {
        _targetTracker = GetComponent<TargetTracker>();
    }

    private void Update()
    {
        _visuals.Rotate(_targetTracker.IsRightSideOf(transform.position));
    }
}