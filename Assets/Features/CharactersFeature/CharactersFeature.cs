using Features.CharactersFeature.Systems;
using FeatureSystem.Features;
using FeatureSystem.Systems;

namespace Features.CharactersFeature
{
    public class CharactersFeature : Feature
    {
        public override void Initialize()
        {
            GameSystems.RegisterSystem(new SpawnCharacterSystem());
            GameSystems.RegisterSystem(new ExperienceSystem());
        }
    }
}