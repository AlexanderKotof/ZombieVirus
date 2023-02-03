using Features.CharactersFeature.Prototypes;
using Features.CharactersFeature.Storage;
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
    }
}