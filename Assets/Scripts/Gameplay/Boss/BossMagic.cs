using UnityEngine;

public class BossMagic : MonoBehaviour
{
    [SerializeField] private Spells _spells;

    public void ExplodeGround(Vector2 position)
    {
        _spells.GetGroundExplodeSpell().Cast(position);
    }
}