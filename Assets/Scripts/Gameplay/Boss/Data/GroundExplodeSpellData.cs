using UnityEngine;

[CreateAssetMenu(menuName = DataPaths.GameplayData.Boss.Attack + "Ground Explode Spell Data")]
public class GroundExplodeSpellData : ScriptableObject
{
    [SerializeField] private float _damage;
    [SerializeField] private float _castDelayTime;
    [SerializeField] private float _spellActiveTime;

    public float Damage => _damage;
    public float CastDelayTime => _castDelayTime;
    public float SpellActiveTime => _spellActiveTime;
}