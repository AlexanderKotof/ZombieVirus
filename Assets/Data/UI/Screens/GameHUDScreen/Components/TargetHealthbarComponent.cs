using Features.CharactersFeature.Components;
using ScreenSystem.Components;
using UnityEngine.UI;

public class TargetHealthbarComponent : WindowComponent
{
    public Slider healthbar;
    public Image icon;
    public TMPro.TMP_Text nameText;

    private CharacterComponent character;

    public void SetInfo(CharacterComponent character)
    {
        this.character = character;

        Show();

        character.HealthChanged += Character_HealthChanged;
        CharacterComponent.Died += Character_Died;

        healthbar.maxValue = character.StartHealth;
        healthbar.value = character.CurrentHealth;

        icon.sprite = character.Prototype.metaData.characterIcon;

        nameText.SetText(character.Prototype.metaData.Name);
    }

    protected override void OnHide()
    {
        if (character)
        {
            character.HealthChanged -= Character_HealthChanged;
            CharacterComponent.Died -= Character_Died;
        }

        base.OnHide();
    }

    private void Character_Died(CharacterComponent obj)
    {
        if (obj == character)
            Hide();
    }

    private void Character_HealthChanged(float value)
    {
        healthbar.value = value;
    }  
}
