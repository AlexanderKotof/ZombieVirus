using Features.CharactersFeature.Components;
using Features.CharactersFeature.Prototypes;
using Features.CharactersFeature.Utils;
using FeatureSystem.Systems;
using PlayerDataSystem.DataStructures;
using System.Collections.Generic;
using UnityEngine;

namespace Features.CharactersFeature.Systems
{
    public class SpawnCharacterSystem : ISystem
    {
        public List<CharacterComponent> spawnedCharacters = new List<CharacterComponent>();
        public List<CharacterComponent> spawnedEnemies = new List<CharacterComponent>();

        public void Initialize()
        {

        }

        public void Destroy()
        {

        }

        public CharacterComponent SpawnPlayerCharacter(CharacterData data, Vector3 position, Quaternion rotation)
        {
            var character = CharacterSpawnUtils.SpawnCharacter(data, position, rotation);
            character.CurrentHealth = data.currentHealth;
            spawnedCharacters.Add(character);
            return character;
        }

        public CharacterComponent SpawnEnemy(CharacterPrototype prototype, Vector3 position, Quaternion rotation)
        {
            var enemy = CharacterSpawnUtils.SpawnCharacter(prototype, position, rotation);
            spawnedEnemies.Add(enemy);
            return enemy;
        }
    }
}
