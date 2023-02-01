using FeatureSystem.Features;
using FeatureSystem.Systems;

public class GamePlayFeature : Feature
{
    public override void Initialize()
    {
        GameSystems.RegisterSystem(new PlayerTeamSystem());
        GameSystems.RegisterSystem(new PlayerCommandSystem());
        GameSystems.RegisterSystem(new EnemyCommandSystem());
        GameSystems.RegisterSystem(new LevelEndSystem());
        GameSystems.RegisterSystem(new GameTimeSystem());
    }
}

