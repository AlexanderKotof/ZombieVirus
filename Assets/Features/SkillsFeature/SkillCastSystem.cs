using FeatureSystem.Systems;
using System.Collections;
using System.Collections.Generic;

public class SkillCastSystem : ISystem
{
    private Dictionary<(CharacterComponent caster, SkillPrototype prototype), float> _skillsOnCooldown; 

    public void Initialize()
    {
        _skillsOnCooldown = new Dictionary<(CharacterComponent caster, SkillPrototype prototype), float>();
    }

    public void CastSkill(CharacterComponent caster, SkillPrototype skill, CharacterComponent target)
    {
        if (GetSkillCooldown(caster, skill) > 0)
            return;

        caster.StartCoroutine(skill.Cast(caster, target));
        caster.StartCoroutine(ReduceCooldown(caster, skill));
    }

    public float GetSkillCooldown(CharacterComponent caster, SkillPrototype skill)
    {
        if (_skillsOnCooldown.TryGetValue((caster, skill), out var cooldown))
        {
            return cooldown;
        }
        return 0;
    }

    private IEnumerator ReduceCooldown(CharacterComponent caster, SkillPrototype skill)
    {
        var tuple = (caster, skill);
        float cooldown = skill.cooldown;

        _skillsOnCooldown.Add(tuple, cooldown);

        while (cooldown > 0)
        {
            yield return null;

            float deltatime = TimeUtils.GetDeltaTime();
            cooldown -= deltatime;
            _skillsOnCooldown[tuple] -= deltatime;
        }

        _skillsOnCooldown.Remove(tuple);
    }

    public void Destroy()
    {
        
    }
}
