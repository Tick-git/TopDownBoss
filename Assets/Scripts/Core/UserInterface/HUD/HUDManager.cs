using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HUDManager : MonoBehaviour
{
    private readonly List<HUDWidget> _widgets = new();

    // =====================
    // TODO: Put this into gameplaySetup

    [SerializeField] private Health _player;
    [SerializeField] private Health _boss;
    [SerializeField] private Stamina _playerStamina;

    private HUDDisplay _hudDisplay;

    private void Awake()
    {
        _hudDisplay = new HUDDisplay(this, _player, _boss, _playerStamina);
        ShowHUD();
    }

    private void OnDestroy()
    {
        _hudDisplay.Dispose();
    }

    private void Update()
    {
        _hudDisplay.Update();
    }

    // =====================

    public void Register(HUDWidget widget)
    {
        _widgets.Add(widget);
        widget.Hide();
    }

    public void Unregister(HUDWidget widget)
    {
        _widgets.Remove(widget);
    }

    public void ShowHUD()
    {
        foreach (HUDWidget widget in _widgets)
        {
            widget.Show();
        }
    }

    public void HideHUD()
    {
        foreach (HUDWidget widget in _widgets)
        {
            widget.Hide();
        }
    }

    public VisualElement GetHUDRoot()
    {
        return GetComponent<UIDocument>().rootVisualElement;
    }
}