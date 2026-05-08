using System;
using UnityEngine.UIElements;

public class HUD : IDisposable
{
    private readonly HUDManager _hudManager;

    private readonly HealthWidget _playerHealthWidget;
    private readonly HealthWidget _bossHealthWidget;

    public HUD(HUDManager hudManager, Health playerHealth, Health bossHealth)
    {
        _hudManager = hudManager;

        var root = hudManager.GetHUDRoot();

        _playerHealthWidget = new HealthWidget(root.Q("PlayerStatsWidget"),"PlayerHealthBar", playerHealth);
        _bossHealthWidget = new HealthWidget(root.Q("BossHealthWidget"),"BossHealthBar", bossHealth);

        _hudManager.Register(_playerHealthWidget);
        _hudManager.Register(_bossHealthWidget);
    }


    public void Dispose()
    {
        _hudManager.Unregister(_playerHealthWidget);
        _hudManager.Unregister(_bossHealthWidget);

        _bossHealthWidget.Dispose();
        _playerHealthWidget.Dispose();
    }
}