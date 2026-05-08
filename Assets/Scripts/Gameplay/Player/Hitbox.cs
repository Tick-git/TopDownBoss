using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private Collider2D _headCollider;

    private const float HeadColliderY = 0.35f;
    private const float RollColliderY = -0.15f;

    public void Initialize()
    {
        _headCollider = GetComponent<CircleCollider2D>();
    }

    public void SetRolling()
    {
        _headCollider.offset = new Vector2(_headCollider.offset.x, RollColliderY);
    }

    public void SetStanding()
    {
        _headCollider.offset = new Vector2(_headCollider.offset.x, HeadColliderY);
    }
}