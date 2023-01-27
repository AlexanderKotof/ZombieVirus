using FeatureSystem.Features;
using FeatureSystem.Systems;
using UnityEngine;

public class CameraFeature : Feature
{
    public CameraRigComponent cameraPrefab;

    public override void Initialize()
    {
        GameSystems.RegisterSystem(new GameCameraSystem(cameraPrefab));
        GameSystems.RegisterSystem(new CameraMovementSystem());
    }
}


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
public class CameraMovementSystem : ISystem
{
    private GameCameraSystem _cameraSystem;
    private PlayerInputSystem _inputSystem;
    public void Destroy()
    {
        _inputSystem.PlayerInput.OnDrag -= MoveCamera;
    }

    public void Initialize()
    {
        _cameraSystem = GameSystems.GetSystem<GameCameraSystem>();
        _inputSystem = GameSystems.GetSystem<PlayerInputSystem>();

        _inputSystem.PlayerInput.OnDrag += MoveCamera;
    }

    private void MoveCamera(Vector3 delta)
    {
        delta = new Vector3(-delta.x, 0, -delta.y) * 0.03f;
        _cameraSystem.Camera.transform.Translate(delta, Space.Self);
    }
}

