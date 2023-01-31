using ScreenSystem.Components;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoComponent : ButtonComponent
{
    public Image selection;
    public Slider healthbar;

    public Image healthbarFill;

    public Gradient healthBarGradient;

    public void SetInfo(PlayerComponent character)
    {
        character.HealthChanged += Character_HealthChanged;

        healthbar.maxValue = character.StartHealth;
        healthbar.value = character.CurrentHealth;

        healthbarFill.color = healthBarGradient.Evaluate(character.CurrentHealth / character.StartHealth);

        image.sprite = character.Data.prototype.metaData.characterIcon;
    }

    private void Character_HealthChanged(float value)
    {
        healthbar.value = value;
        healthbarFill.color = healthBarGradient.Evaluate(healthbar.value / healthbar.maxValue);
    }

    public void SwitchSelection(bool selected)
    {
        selection.enabled = selected;
    }
}
