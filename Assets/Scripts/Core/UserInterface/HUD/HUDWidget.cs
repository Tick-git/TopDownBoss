using System;
using UnityEngine.UIElements;

public abstract class HUDWidget : IDisposable
{
    private readonly VisualElement _root;

    protected HUDWidget(VisualElement root)
    {
        _root = root;
    }

    public void Show()
    {
        _root.style.display = DisplayStyle.Flex;
    }

    public void Hide()
    {
        _root.style.display = DisplayStyle.None;
    }

    public virtual void Dispose()
    {
        // NOOP
    }
}