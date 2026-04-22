using System;
using UnityEngine.UIElements;

public abstract class View : IDisposable
{
    private readonly VisualElement _root;

    protected View(VisualElement root)
    {
        this._root = root;
    }

    public virtual void Show()
    {
        _root.style.display = DisplayStyle.Flex;
    }

    public virtual void Hide()
    {
        _root.style.display = DisplayStyle.None;
    }

    public virtual void Dispose()
    {
    }

    public virtual bool TryHandleCancel()
    {
        return false;
    }
}