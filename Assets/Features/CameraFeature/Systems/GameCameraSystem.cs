using FeatureSystem.Systems;

namespace Features.CameraFeature.Systems
{
    public class GameCameraSystem : ISystem
    {
        public CameraRigComponent Camera { get; private set; }
        private CameraRigComponent _cameraPrefab;

        public GameCameraSystem(CameraRigComponent cameraPrefab)
        {
            _cameraPrefab = cameraPrefab;
        }

        public void Destroy()
        {
            ObjectSpawnManager.Despawn(Camera);
        }

        public void Initialize()
        {
            Camera = ObjectSpawnManager.Spawn(_cameraPrefab);
        }
    }
}