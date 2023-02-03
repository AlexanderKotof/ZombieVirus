using Features.CharactersFeature.Components;
using Features.CharactersFeature.Systems;
using FeatureSystem.Systems;
using System;
using UnityEngine;

namespace Features.GamePlayFeature.Data
{
    [Serializable]
    public class PlayerTeam
    {
        public PlayerComponent[] characters { get; private set; }

        public CharacterData[] charactersData;

        public void InstatiateCharacters(Transform spawnPoint)
        {
            var characterSystem = GameSystems.GetSystem<SpawnCharacterSystem>();

            charactersData = PlayerDataManager.Data.characterDatas;

            characters = new PlayerComponent[charactersData.Length];

            for (int i = 0; i < charactersData.Length; i++)
            {
                var position = spawnPoint.position + Vector3.right * i;
                characters[i] = characterSystem.SpawnPlayerCharacter(charactersData[i], position, spawnPoint.rotation) as PlayerComponent;
            }
        }


    }
}