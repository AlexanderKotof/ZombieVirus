using Features.SkillsFeature.Prototypes;
using Features.SkillsFeature.Systems;
using FeatureSystem.Features;
using FeatureSystem.Systems;
using System.Collections.Generic;

namespace Features.SkillsFeature
{
    public class SkillsFeature : Feature
    {
        public List<SkillPrototype> skills;

        public override void Initialize()
        {
            GameSystems.RegisterSystem(new SkillCastSystem());
        }
    }
}