using SaveSystem.DataStructures;
using System;

namespace PlayerDataSystem.DataStructures
{
    [Serializable]
    public class PlayerData
    {
        public CharacterData[] characterDatas;

        public InventoryData inventoryData;

        public QuestsData questsData;

        // not used
        // public BuildingsSaveData buildingsData;

        public string currentScene;
    }
}