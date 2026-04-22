using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HUDManager : MonoBehaviour
{
    private readonly List<HUDWidget> widgets  = new();

    public void Register(HUDWidget widget)
    {
        widgets.Add(widget);
        widget.Hide();
    }

    public void ShowHUD()
    {
        foreach (HUDWidget widget in widgets)
        {
            widget.Show();
        }
    }

    public void HideHUD()
    {
        foreach (HUDWidget widget in widgets)
        {
            widget.Hide();
        }
    }

    public VisualElement GetHUDRoot()
    {
        return GetComponent<UIDocument>().rootVisualElement;
    }
}