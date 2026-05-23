using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private Collider2D[] _colliders;

    private void Awake()
    {
        _colliders = GetComponents<Collider2D>();
    }

    public void EnableHitbox()
    {
        foreach (var col in _colliders)
        {
            col.enabled = true;
        }
    }

    public void DisableHitbox()
    {
        foreach (var col in _colliders)
        {
            col.enabled = false;
        }
    }
}