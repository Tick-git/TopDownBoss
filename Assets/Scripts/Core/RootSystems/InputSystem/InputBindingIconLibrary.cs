using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = DataPaths.CoreData.Input + "Binding Icons Database")]
public class InputBindingIconLibrary : ScriptableObject
{
    [SerializeField] private InputBindingIconsData interact;

    public InputBindingIconsData Interact => interact;

    private readonly Dictionary<string, InputBindingIconsData> lookup = new();


    public void OnEnable()
    {
        if (interact != null)
            lookup.Add(InputActionName.Player.Interact, Interact);
    }

    public Texture GetBindingIcon(string actionName, InputDeviceType deviceType)
    {
        if (!lookup.TryGetValue(actionName, out var bindingIcons))
            return null;

        return deviceType switch
        {
            InputDeviceType.Mouse => bindingIcons.KeyboardMouse,
            InputDeviceType.Keyboard => bindingIcons.KeyboardMouse,
            InputDeviceType.DualShock => bindingIcons.DualShock,
            InputDeviceType.XInput => bindingIcons.XInput,
            _ => null
        };
    }
}