using ScreenSystem.Components;
using UnityEngine.UI;

public class CharacterInfoComponent : ButtonComponent
{
    public Image selection;
    public Slider healthbar;

    public void SetInfo(PlayerComponent character)
    {
        character.HealthChanged += Character_HealthChanged;

        healthbar.maxValue = character.StartHealth;
        healthbar.value = character.CurrentHealth;

        image.sprite = character.Data.prototype.metaData.characterIcon;
    }

    private void Character_HealthChanged(float value)
    {
        healthbar.value = value;
    }

    public void SwitchSelection(bool selected)
    {
        selection.enabled = selected;
    }
}
