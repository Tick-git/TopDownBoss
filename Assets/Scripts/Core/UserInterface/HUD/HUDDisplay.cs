using System;
using Core.UserInterface.HUD;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class HUDDisplay : IDisposable
{
    private readonly HUDManager _hudManager;

    private readonly HealthWidget _playerHealthWidget;
    private readonly HealthWidget _bossHealthWidget;
    private readonly StaminaWidget _playerStaminaWidget;
    private readonly Stamina _playerStamina;

    public HUDDisplay(HUDManager hudManager, Health playerHealth, Health bossHealth, Stamina playerStamina)
    {
        _hudManager = hudManager;
        _playerStamina = playerStamina;

        var root = hudManager.GetHUDRoot();
        var playerStats = root.Q("PlayerStatsWidget");

        _playerHealthWidget = new HealthWidget(playerStats, "PlayerHealthBar", playerHealth);
        _playerStaminaWidget = new StaminaWidget(playerStats, "PlayerStaminaBar", _playerStamina.MaxStamina);
        _bossHealthWidget = new HealthWidget(root.Q("BossHealthWidget"), "BossHealthBar", bossHealth);

        _hudManager.Register(_playerHealthWidget);
        _hudManager.Register(_bossHealthWidget);
    }

    public void Update()
    {
        _playerStaminaWidget.SetStamina(_playerStamina.CurrentStamina);
    }


    public void Dispose()
    {
        _hudManager.Unregister(_playerHealthWidget);
        _hudManager.Unregister(_bossHealthWidget);

        _bossHealthWidget.Dispose();
        _playerHealthWidget.Dispose();
    }
}