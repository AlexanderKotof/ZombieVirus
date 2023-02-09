using Features.CharactersFeature.Components;
using Features.CharactersFeature.Systems;
using Features.GamePlayFeature.Data;
using FeatureSystem.Systems;
using PlayerDataSystem.DataStructures;
using TeamSystem;
using UnityEngine;

namespace Features.GamePlayFeature.Systems
{
    public class PlayerTeamSystem : ISystem
    {
        public PlayerComponent[] Characters { get; private set; }

        public CharacterData[] charactersData;

        public int selectedCharacters = -1;

        public void Initialize()
        {
            var sceneContext = SceneContext.GetSceneContext();
            InstatiateCharacters(sceneContext.playerSpawnPoint);
        }

        public void InstatiateCharacters(Transform spawnPoint)
        {
            var characterSystem = GameSystems.GetSystem<SpawnCharacterSystem>();

            charactersData = PlayerTeamManager.Instance.GetCharactersOnLevel();
            Characters = new PlayerComponent[charactersData.Length];

            for (int i = 0; i < charactersData.Length; i++)
            {
                var position = spawnPoint.position + Vector3.right * i;
                Characters[i] = characterSystem.SpawnPlayerCharacter(charactersData[i], position, spawnPoint.rotation) as PlayerComponent;
            }
        }

        public CharacterComponent[] GetSelectedCharacters()
        {
            if (selectedCharacters == -1)
            {
                return Characters;
            }
            else
            {
                return new[] { Characters[selectedCharacters] };
            }
        }


        public void Destroy()
        {
            // destroy characters
        }
    }
}