using UnityEngine.UIElements;

public abstract class HUDWidget
{
    private readonly VisualElement root;

    protected HUDWidget(VisualElement root)
    {
        this.root = root;
    }

    public void Show()
    {
        root.style.display = DisplayStyle.Flex;
    }

    public void Hide()
    {
        root.style.display = DisplayStyle.None;
    }
}