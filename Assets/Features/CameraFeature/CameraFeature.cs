using Features.CameraFeature.Systems;
using FeatureSystem.Features;
using FeatureSystem.Systems;

namespace Features.CameraFeature
{
    public class CameraFeature : Feature
    {
        public CameraRigComponent cameraPrefab;

        public float movementSpeed = 5f;

        public override void Initialize()
        {
            GameSystems.RegisterSystem(new GameCameraSystem(cameraPrefab));
            GameSystems.RegisterSystem(new CameraMovementSystem(this));
        }
    }
}