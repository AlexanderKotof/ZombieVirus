using Features.InputFeature.Systems;
using FeatureSystem.Features;
using FeatureSystem.Systems;

namespace Features.InputFeature
{
    public class GameInputFeature : Feature
    {
        public override void Initialize()
        {
            GameSystems.RegisterSystem(new PlayerInputSystem());
        }
    }
}