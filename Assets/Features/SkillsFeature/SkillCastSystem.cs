using FeatureSystem.Systems;

public class SkillCastSystem : ISystem
{


    public void Initialize()
    {
       
    }

    public void CastSkill(CharacterComponent caster, SkillPrototype skill, CharacterComponent target)
    {
        caster.StartCoroutine(skill.Cast(caster, target));
    }

    public void Destroy()
    {
        
    }
}
