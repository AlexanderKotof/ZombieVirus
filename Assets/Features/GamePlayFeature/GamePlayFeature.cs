using FeatureSystem.Features;
using FeatureSystem.Systems;
using System;
using UnityEngine;

public class GamePlayFeature : Feature
{
    public override void Initialize()
    {
        GameSystems.RegisterSystem(new PlayerTeamSystem());
        GameSystems.RegisterSystem(new CharactersCommandSystem());
    }
}

public class CharactersCommandSystem : ISystem
{
    private InputController _input;
    private CameraRigComponent _cameraRig;
    private PlayerTeamSystem _playerTeam;


    public void Destroy()
    {
        _input.SwitchCharacterSelection -= SwitchSelection;
        _input.OnTap -= CommandPlayerCharacters;
    }

    public void Initialize()
    {
        _input = GameSystems.GetSystem<PlayerInputSystem>().PlayerInput;
        _cameraRig = GameSystems.GetSystem<GameCameraSystem>().Camera;
        _playerTeam = GameSystems.GetSystem<PlayerTeamSystem>();

        _input.OnTap += CommandPlayerCharacters;
        _input.SwitchCharacterSelection += SwitchSelection;
    }
    private void SwitchSelection(int obj)
    {
        _playerTeam.selectedCharacters = obj;
    }

    private void CommandPlayerCharacters(Vector3 obj)
    {
        var ray = _cameraRig.camera.ScreenPointToRay(obj);

        if (Physics.Raycast(ray, out var hit))
        {
            if (hit.collider.TryGetComponent<EnemyComponent>(out var enemy))
            {
                ScreenSystem.ScreensManager.GetScreen<GameHUDScreen>().SetTarget(enemy);

                foreach (var character in _playerTeam.GetSelectedCharacters())
                {
                    var command = new AttackCommand(enemy);
                    character.ExecuteCommand(command);
                }
            }
            else
            {
                ScreenSystem.ScreensManager.GetScreen<GameHUDScreen>().SetTarget(null);

                var characters = _playerTeam.GetSelectedCharacters();
                for (int i = 0; i < characters.Length; i++)
                {
                    CharacterComponent character = characters[i];
                    var command = new MovingCommand(hit.point + Vector3.right * i);
                    character.ExecuteCommand(command);
                }
            }
        }
    }



}
