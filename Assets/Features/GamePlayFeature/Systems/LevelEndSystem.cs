using FeatureSystem.Systems;
using System;
using UnityEngine;

namespace Features.GamePlayFeature.Systems
{
    public class LevelEndSystem : ISystem, ISystemUpdate
    {
        private Transform _levelEndPoint;
        private PlayerTeamSystem _playerTeamSystem;

        private float _checkDistance = 2f;

        public static event Action LevelEnded;
        public static event Action LevelFailed;

        public void Initialize()
        {
            _levelEndPoint = SceneContext.GetSceneContext().exitPoint;
            _playerTeamSystem = GameSystems.GetSystem<PlayerTeamSystem>();
        }

        public void Destroy()
        {

        }

        public void Update()
        {
            int charactersCount = 0;
            int diedCharacters = 0;
            foreach (var character in _playerTeamSystem.Characters)
            {
                if (character.IsDied)
                {
                    diedCharacters++;
                    continue;
                }

                if ((character.Position - _levelEndPoint.position).sqrMagnitude < _checkDistance * _checkDistance)
                {
                    charactersCount++;
                }
            }

            if (diedCharacters == _playerTeamSystem.Characters.Length)
            {
                LevelFailed?.Invoke();
                return;
            }

            if (charactersCount > 0 && charactersCount + diedCharacters == _playerTeamSystem.Characters.Length)
            {
                LevelEnded?.Invoke();
            }
        }
    }
}