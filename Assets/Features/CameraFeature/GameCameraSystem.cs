using FeatureSystem.Systems;

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
        CameraRigComponent.Destroy(Camera);
    }

    public void Initialize()
    {
        Camera = CameraRigComponent.Instantiate(_cameraPrefab, null);
    }
}

