using UnityEngine;

[CreateAssetMenu(menuName = DataPaths.CoreData.Input + "BindingIcons")]
public class InputBindingIconsData : ScriptableObject
{
    [SerializeField] Texture keyboardMouse;
    [SerializeField] Texture xInput;
    [SerializeField] Texture dualShock;
    [SerializeField] Texture switchPro;
    
    public Texture KeyboardMouse => keyboardMouse;
    public Texture XInput => xInput;
    public Texture DualShock => dualShock;
    public Texture SwitchPro => switchPro;
}
