using UnityEngine;

public class GroundAttackAnimationEvents : MonoBehaviour
{
    [SerializeField] private GroundAttackFeedback _groundAttackFeedback;

    public void PlayHandFire() => _groundAttackFeedback.PlayHandFire();

    public void StopHandFire() => _groundAttackFeedback.StopHandFire();

    public void PlayGroundImpact() => _groundAttackFeedback.PlayGroundImpact();
}