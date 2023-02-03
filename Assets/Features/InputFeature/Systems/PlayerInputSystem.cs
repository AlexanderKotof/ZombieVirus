using Features.InputFeature.Controller;
using FeatureSystem.Systems;

namespace Features.InputFeature.Systems
{
    public class PlayerInputSystem : ISystem
    {
        public InputController PlayerInput { get; private set; }

        public void Initialize()
        {
            PlayerInput = new InputController();
        }
        public void Destroy()
        {
            PlayerInput.Dispose();
        }
    }
}