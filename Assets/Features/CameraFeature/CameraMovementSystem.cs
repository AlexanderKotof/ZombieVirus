using FeatureSystem.Systems;
using System;
using UnityEngine;

public class CameraMovementSystem : ISystem, ISystemUpdate
{
    private GameCameraSystem _cameraSystem;
    private PlayerInputSystem _inputSystem;

    private Vector3 _destination;

    private CameraFeature _feature;

    private Bounds _cameraBounds;

    public CameraMovementSystem(CameraFeature feature)
    {
        _feature = feature;
    }

    public void Initialize()
    {
        _cameraSystem = GameSystems.GetSystem<GameCameraSystem>();
        _inputSystem = GameSystems.GetSystem<PlayerInputSystem>();

        _inputSystem.PlayerInput.OnDrag += MoveCamera;

        var sceneContext = SceneContext.GetSceneContext();
        _cameraBounds = sceneContext.cameraBounds;

        SetCameraPosition(sceneContext.playerSpawnPoint.position);
    }

    public void Destroy()
    {
        _inputSystem.PlayerInput.OnDrag -= MoveCamera;
    }

    public void Update()
    {
        var cameraTransform = _cameraSystem.Camera.transform;
        if (_destination != cameraTransform.position)
        {
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, _destination, _feature.movementSpeed * Time.unscaledDeltaTime);
        }
    }

    private void MoveCamera(Vector3 delta)
    {
        delta = new Vector3(-delta.x, 0, -delta.y) * 0.03f;

        _destination += delta;

        if (!_cameraBounds.Contains(_destination))
        {
            ClampDestination();
        }
    }

    private void ClampDestination()
    {
        _destination.x = Mathf.Clamp(_destination.x, _cameraBounds.min.x, _cameraBounds.max.x);
        _destination.z = Mathf.Clamp(_destination.z, _cameraBounds.min.z, _cameraBounds.max.z);
    }

    public void SetCameraPosition(Vector3 position)
    {
        _destination = position;
        _cameraSystem.Camera.transform.position = position;
    }
}

