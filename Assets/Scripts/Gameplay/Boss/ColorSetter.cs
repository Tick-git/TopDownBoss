using UnityEngine;

public class ColorSetter : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private bool _redColorEnabled = false;
    private Color _color;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetColor(Color color)
    {
        _redColorEnabled = true;
        _color = color;
    }
    
    private void LateUpdate()
    {
        if (!_redColorEnabled) return;
        
        _spriteRenderer.color = new Color(_color.r, _color.g, _color.b, _spriteRenderer.color.a);
    }
}
