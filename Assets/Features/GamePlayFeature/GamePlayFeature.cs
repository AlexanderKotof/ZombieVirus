using Features.GamePlayFeature.Systems;
using FeatureSystem.Features;
using FeatureSystem.Systems;

namespace Features.GamePlayFeature
{
    public class GamePlayFeature : Feature
    {
        public float accuracyChanceForHeadshotReaquired = 0.8f;
        public float headShotDamageMultiplier = 5;

        public override void Initialize()
        {
            GameSystems.RegisterSystem(new PlayerTeamSystem());
            GameSystems.RegisterSystem(new PlayerCommandSystem());
            GameSystems.RegisterSystem(new EnemyCommandSystem());
            GameSystems.RegisterSystem(new DealDamageSystem(this));
            GameSystems.RegisterSystem(new LevelEndSystem());
            GameSystems.RegisterSystem(new GameTimeSystem());
        }
    }
}
