using UnityEngine;

public class ViewAnimationController : MonoBehaviour
{
    private ViewStack _viewStack;
    private IAnimatableView _animatableView;
    
    public void Initialize(ViewStack viewStack)
    {
        _viewStack = viewStack;

        _viewStack.ActiveViewChanged += OnActiveViewChanged;
    }

    public void OnDestroy()
    {
        _viewStack.ActiveViewChanged -= OnActiveViewChanged;
    }

    private void OnActiveViewChanged(ActiveViewChangedArgs args)
    {
        _animatableView = args.CurrentActiveView as IAnimatableView;
    }

    private void Update()
    {
        _animatableView?.Update();
    }
}