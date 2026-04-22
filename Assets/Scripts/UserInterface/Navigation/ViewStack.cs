using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ViewStack
{
    private readonly Dictionary<Type, View> _views = new();
    private readonly Stack<View> _viewStack = new();
    public event Action<ActiveViewChangedArgs> ActiveViewChanged;

    private readonly VisualElement _rootVisualElement;
    
    public ViewStack(VisualElement rootVisualElement)
    {
        _rootVisualElement = rootVisualElement;
    }

    public void Register(View view)
    {
        var type = view.GetType();

        if (!_views.TryAdd(type, view))
        {
            Debug.LogWarning("View already registered: " + type);
            return;
        }
        
        view.Hide();
    }

    public void Unregister(View view)
    {
        var type = view.GetType();

        if (!_views.Remove(type))
        {
            Debug.LogWarning("View not registered in navigation stack: " + type);
            return;
        }

        RemoveFromStack(view);
        view.Hide();
    }

    public void Push<T>() where T : View
    {
        var nextView = GetView<T>();
        View prevView = null;
        
        if (_viewStack.Count > 0 && _viewStack.Peek() == nextView)
        {
            Debug.LogWarning($"You cannot push more than one {nextView.GetType()} on top of the stack at a time");
            return;
        }
        
        if (_viewStack.Count > 0)
        {
            prevView = _viewStack.Peek();
            prevView.Hide();
        }

        nextView.Show();
        _viewStack.Push(nextView);

        ActiveViewChanged?.Invoke(new ActiveViewChangedArgs(prevView, nextView));
    }

    public void Pop()
    {
        if (_viewStack.Count == 0) return;

        View poppedView = _viewStack.Pop();
        poppedView.Hide();

        if (_viewStack.Count > 0)
        {
            ActiveView.Show();
        }
        
        ActiveViewChanged?.Invoke(new ActiveViewChangedArgs(poppedView, ActiveView));
    }

    public void Clear()
    {
        while (_viewStack.Count > 0)
        {
            Pop();
        }
    }
    
    public void HandleCancel()
    {
        if (_viewStack.Count == 0) return;

        if (!ActiveView.TryHandleCancel())
        {
            Pop();
        }
    }
    
    public VisualElement GetUIRoot()
    {
        return _rootVisualElement;
    }
    
    private void RemoveFromStack(View target)
    {
        var temp = new Stack<View>();
        View removedView = null;

        while (_viewStack.Count > 0)
        {
            var view = _viewStack.Pop();

            if (view == target)
                removedView = view;
            else
                temp.Push(view);
        }

        while (temp.Count > 0)
            _viewStack.Push(temp.Pop());

        if (removedView != null)
            ActiveViewChanged?.Invoke(new ActiveViewChangedArgs(removedView, ActiveView));
    }
    
    private View GetView<T>() where T : View
    {
        if (!_views.TryGetValue(typeof(T), out View view))
        {
            Debug.LogError("View not registered: " + typeof(T));
        }

        return view;
    }

    public View ActiveView
    {
        get
        {
            if (_viewStack.TryPeek(out View view))
                return view;

            return null;
        }
    }

    public bool HasActiveView => ActiveView != null;
    public bool IsEmpty => ActiveView == null;
}

public readonly struct ActiveViewChangedArgs
{
    public View PreviousActiveView { get; }
    public View CurrentActiveView { get; }

    public ActiveViewChangedArgs(View previousActiveView, View currentActiveView)
    {
        PreviousActiveView = previousActiveView;
        CurrentActiveView = currentActiveView;
    }
}

public enum InteractionModeUI
{
    Locked,
    Hover,
    Focus,
    None
}