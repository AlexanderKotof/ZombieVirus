using Features.CharactersFeature.Components;
using Features.SkillsFeature.Prototypes;
using Features.SkillsFeature.Systems;
using FeatureSystem.Systems;
using ScreenSystem.Components;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoComponent : ButtonComponent
{
    public Image selection;
    public Slider healthbar;

    public Image healthbarFill;

    public Gradient healthBarGradient;

    public ListComponent characterSkills;

    public void SetInfo(PlayerComponent character)
    {
        character.HealthChanged += Character_HealthChanged;

        healthbar.maxValue = character.StartHealth;
        healthbar.value = character.CurrentHealth;

        healthbarFill.color = healthBarGradient.Evaluate(character.CurrentHealth / character.StartHealth);

        image.sprite = character.Prototype.metaData.characterIcon;

        characterSkills.SetItems<SkilListItemComponent>(character.Prototype.skills.Length, (item, par) =>
        {
           var skill = character.Prototype.skills[par.index];
           item.SetInfo(skill, () => CastSkill(skill, character));
        });
    }

    private void CastSkill(SkillPrototype prototype, CharacterComponent caster)
    {
        var skillCastSystem = GameSystems.GetSystem<SkillCastSystem>();
        var target = caster.target;

        if (target == null)
        {
            return;
        }

        skillCastSystem.CastSkill(caster, prototype, target);
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
