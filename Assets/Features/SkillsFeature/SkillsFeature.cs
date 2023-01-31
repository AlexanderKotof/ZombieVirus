using FeatureSystem.Features;
using FeatureSystem.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsFeature : Feature
{
    public List<SkillPrototype> skills;

    public override void Initialize()
    {
        GameSystems.RegisterSystem(new SkillCastSystem());
    }
}
