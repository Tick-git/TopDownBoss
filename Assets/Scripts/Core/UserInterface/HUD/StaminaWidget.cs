using UnityEngine.UIElements;

namespace Core.UserInterface.HUD
{
    public class StaminaWidget : HUDWidget
    {
        private readonly Slider _staminaBar;
        
        public StaminaWidget(VisualElement root, string barRootName, float maxStamina) : base(root)
        {
            _staminaBar = root.Q(barRootName).Q<Slider>("FillableBar");
            _staminaBar.highValue = maxStamina;
        }

        public void SetStamina(float value)
        {
            _staminaBar.value = value;
        }
    }
}