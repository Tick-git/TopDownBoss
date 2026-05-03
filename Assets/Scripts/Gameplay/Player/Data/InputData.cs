using UnityEngine;

[CreateAssetMenu(menuName = DataPaths.GameplayData.PlayerPaths + "InputData")]
public class InputData : ScriptableObject
{
    [SerializeField] private float _rollBufferDuration;
    public float RollBufferDuration => _rollBufferDuration;
}