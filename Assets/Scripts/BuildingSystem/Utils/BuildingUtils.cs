using BuildingSystem.Prototypes;
using BuildingSystem.Storage;
using UnityEngine;

namespace BuildingSystem.Utils
{
    public static class BuildingUtils
    {
        private static BuildingStorage _storage;

        public static BuildingPrototype GetItem(int id)
        {
            if (_storage == null)
                LoadStorage();

            return _storage.GetItem(id);
        }

        private static void LoadStorage()
        {
            _storage = Resources.Load<BuildingStorage>(nameof(BuildingStorage));
        }
    }
}