using Features.CharactersFeature.Components;
using Features.CharactersFeature.Prototypes;
using PlayerDataSystem.DataStructures;
using UnityEngine;

namespace Features.CharactersFeature.Utils
{
    public static class CharacterSpawnUtils
    {
        public static CharacterComponent SpawnCharacter(CharacterData data, Vector3 position, Quaternion rotation)
        {
            var character = ObjectSpawnManager.Spawn(data.prototype.characterPrefab);
            character.transform.position = position;
            character.transform.rotation = rotation;

            character.SetData(data);
            return character;
        }

        public static CharacterComponent SpawnCharacter(CharacterPrototype prototype, Vector3 position, Quaternion rotation)
        {
            var character = ObjectSpawnManager.Spawn(prototype.characterPrefab);
            character.transform.position = position;
            character.transform.rotation = rotation;

            var data = new CharacterData()
            {
                currentHealth = prototype.health,
                currentExp = 0,
                prototype = prototype,
            };
            character.SetData(data);
            return character;
        }
    }
}