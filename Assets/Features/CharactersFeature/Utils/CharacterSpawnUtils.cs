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
            var prototype = CharactersUtils.GetPrototype(data.prototypeId);
            var character = ObjectSpawnManager.Spawn(prototype.characterPrefab);
            character.transform.position = position;
            character.transform.rotation = rotation;

            character.SetData(data, prototype);
            return character;
        }

        public static CharacterComponent SpawnCharacter(CharacterPrototype prototype, Vector3 position, Quaternion rotation)
        {
            var character = ObjectSpawnManager.Spawn(prototype.characterPrefab);
            character.transform.position = position;
            character.transform.rotation = rotation;

            var data = new CharacterData()
            {
                maxHealth = prototype.health,
                currentHealth = prototype.health,
                currentExp = 0,
                prototypeId = prototype.Id,
                weaponId = prototype.defaultWeapon ? prototype.defaultWeapon.Id : - 1,
                armorId = prototype.defaultArmor ? prototype.defaultArmor.Id : -1,
            };

            character.SetData(data, prototype);
            return character;
        }
    }
}