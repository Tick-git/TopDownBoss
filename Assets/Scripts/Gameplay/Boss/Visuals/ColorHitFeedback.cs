using UnityEngine;

public class ColorHitFeedback : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private SpriteRenderer[] _spriteRenderers;

    private SpriteRendererData[] _spriteRendererData;

    private readonly float _speed = 10f;
    private readonly Color _hitColor = Color.red;
    private float _time;

    private const float FullyRecoveredTime = 1f;

    private void Awake()
    {
        _spriteRendererData = new SpriteRendererData[_spriteRenderers.Length];

        for (int i = 0; i < _spriteRendererData.Length; i++)
        {
            var curRenderer = _spriteRenderers[i];
            _spriteRendererData[i] = new SpriteRendererData(curRenderer, curRenderer.color);
        }

        _time = FullyRecoveredTime;
    }

    private void OnEnable()
    {
        _health.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float _)
    {
        SetSpriteRenderersColor(_hitColor);
        _time = 0;
    }

    private void Update()
    {
        _time = Mathf.Clamp01(_time + Time.deltaTime * _speed);

        LerpSpritesToDefaultColor(_time);
    }

    private void SetSpriteRenderersColor(Color hitColor)
    {
        foreach (var data in _spriteRendererData)
        {
            data.SpriteRenderer.color = hitColor;
        }
    }

    private void LerpSpritesToDefaultColor(float time)
    {
        foreach (var data in _spriteRendererData)
        {
            data.SpriteRenderer.color = Color.Lerp(_hitColor, data.DefaultColor, time);
        }
    }

    private struct SpriteRendererData
    {
        public readonly SpriteRenderer SpriteRenderer;
        public readonly Color DefaultColor;

        public SpriteRendererData(SpriteRenderer spriteRenderer, Color defaultColor)
        {
            SpriteRenderer = spriteRenderer;
            DefaultColor = defaultColor;
        }
    }
}