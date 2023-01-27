using FeatureSystem.Features;
using FeatureSystem.Systems;

public class GameInputFeature : Feature
{
    public override void Initialize()
    {
        GameSystems.RegisterSystem(new PlayerInputSystem());
    }
}

public class PlayerInputSystem : ISystem
{
    public InputController PlayerInput { get; private set; }

    public void Initialize()
    {
        PlayerInput = new InputController();
    }
    public void Destroy()
    {

    }
}
