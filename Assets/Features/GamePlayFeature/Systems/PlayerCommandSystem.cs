using Features.CameraFeature.Systems;
using Features.CharactersFeature.Components;
using Features.InputFeature.Controller;
using Features.InputFeature.Systems;
using FeatureSystem.Systems;
using System.Collections.Generic;
using UnityEngine;

namespace Features.GamePlayFeature.Systems
{
    public class PlayerCommandSystem : ISystem
    {
        private InputController _input;
        private CameraRigComponent _cameraRig;
        private PlayerTeamSystem _playerTeam;

        private Dictionary<CharacterComponent, Command> _characterCommands;

        public void Initialize()
        {
            _input = GameSystems.GetSystem<PlayerInputSystem>().PlayerInput;
            _cameraRig = GameSystems.GetSystem<GameCameraSystem>().Camera;
            _playerTeam = GameSystems.GetSystem<PlayerTeamSystem>();

            _characterCommands = new Dictionary<CharacterComponent, Command>();

            _input.OnTap += CommandPlayerCharacters;
            _input.SwitchCharacterSelection += SwitchSelection;
        }

        public void Destroy()
        {
            _input.SwitchCharacterSelection -= SwitchSelection;
            _input.OnTap -= CommandPlayerCharacters;

            _cameraRig = null;
            _input = null;
            _playerTeam = null;
        }

        private void SwitchSelection(int obj)
        {
            _playerTeam.selectedCharacters = obj;
        }

        private void CommandPlayerCharacters(Vector3 obj)
        {
            var ray = _cameraRig.Camera.ScreenPointToRay(obj);

            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.TryGetComponent<EnemyComponent>(out var enemy))
                {
                    SetAttackCommand(enemy);
                }
                else
                {
                    SetMoveCommand(hit.point);
                }
            }
        }

        private void SetAttackCommand(CharacterComponent target)
        {
            ScreenSystem.ScreensManager.GetScreen<GameHUDScreen>().SetTarget(target);

            foreach (var character in _playerTeam.GetSelectedCharacters())
            {
                if (character.IsDied)
                    continue;

                var command = new AttackCommand(target);

                _characterCommands[character] = command;

                character.ExecuteCommand(command);
            }
        }

        private void SetMoveCommand(Vector3 destination)
        {
            ScreenSystem.ScreensManager.GetScreen<GameHUDScreen>().SetTarget(null);

            var characters = _playerTeam.GetSelectedCharacters();
            for (int i = 0; i < characters.Length; i++)
            {
                CharacterComponent character = characters[i];
                if (character.IsDied)
                    continue;

                var command = new MovingCommand(destination + Vector3.right * i);
                _characterCommands[character] = command;
                character.ExecuteCommand(command);
            }
        }

        public Command GetCharacterCommand(CharacterComponent character)
        {
            if (_characterCommands.TryGetValue(character, out var command))
                return command;

            return null;
        }
    }
}