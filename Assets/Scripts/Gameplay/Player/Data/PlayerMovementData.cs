using UnityEngine;

[CreateAssetMenu(menuName = DataPaths.GameplayData.PlayerPaths + "Movement")]
public class PlayerMovementData : ScriptableObject
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rollSpeed;
    
    public float MoveSpeed => _moveSpeed;
    public float RollSpeed => _rollSpeed;
}