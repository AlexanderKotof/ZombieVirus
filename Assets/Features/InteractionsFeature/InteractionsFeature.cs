using Features.InteractionFeature.Systems;
using FeatureSystem.Features;
using FeatureSystem.Systems;

namespace Features.InteractionFeature
{
    public class InteractionsFeature : Feature
    {
        public override void Initialize()
        {
            GameSystems.RegisterSystem(new InteractionSystem());
        }
    }
}