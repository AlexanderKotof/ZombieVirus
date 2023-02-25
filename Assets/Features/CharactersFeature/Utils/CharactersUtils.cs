using Features.CharactersFeature.Prototypes;
using Features.CharactersFeature.Storage;
using PlayerDataSystem.DataStructures;
using UnityEngine;

namespace Features.CharactersFeature.Utils
{
    public static class CharactersUtils
    {
        private static CharactersStorage _storage;

        private static void LoadStorage()
        {
            _storage = Resources.Load<CharactersStorage>(nameof(CharactersStorage));
        }

        public static CharacterPrototype GetPrototype(int id)
        {
            if (_storage == null)
                LoadStorage();

            return _storage.GetItem(id);
        }

        public static string GetCharacterDamage(CharacterData data)
        {
            var prototype = GetPrototype(data.prototypeId);

            var weapon = data.weaponId != -1 ? InventoryUtils.GetItem(data.weaponId) as Weapon : null;

            return data.weaponId == -1 ? Mathf.RoundToInt(prototype.damage).ToString() : Mathf.RoundToInt(weapon.Damage).ToString();
        }

        public static string GetCharacterDefence(CharacterData data)
        {
            var prototype = GetPrototype(data.prototypeId);

            var armor = data.armorId != -1 ? InventoryUtils.GetItem(data.armorId) as Armor : null;

            return data.weaponId == -1 ? Mathf.RoundToInt(prototype.defence).ToString() : Mathf.RoundToInt(armor.defence).ToString();
        }
    }
}