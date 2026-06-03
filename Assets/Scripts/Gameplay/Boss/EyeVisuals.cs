using System;
using UnityEngine;

public class EyeVisuals : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private bool _redColorEnabled = false;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetEyeColorToRed()
    {
        _redColorEnabled = true;
    }
    
    private void LateUpdate()
    {
        if (!_redColorEnabled) return;
        
        _spriteRenderer.color = new Color(1, 0, 0, _spriteRenderer.color.a);
    }
}
