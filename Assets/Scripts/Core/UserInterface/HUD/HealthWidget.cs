using UnityEngine.UIElements;

public class HealthWidget : HUDWidget
{
    private readonly Slider _healthBar;
    private readonly Health _health;
    
    public HealthWidget(VisualElement root, Health health) : base(root)
    {
        _health = health;
        
        _healthBar = root.Q<Slider>("HealthBar");
        _health.HealthChanged += SetHealth;
        
        SetHealth(_health.MaxHealth);
    }
    
    public override void Dispose()
    {
        base.Dispose();
        
        _health.HealthChanged -= SetHealth;
    }

    private void SetHealth(float health)
    {
        _healthBar.value = health / _health.MaxHealth * 100f;
        
    }
}
