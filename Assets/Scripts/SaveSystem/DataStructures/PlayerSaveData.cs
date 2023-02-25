using System;

namespace SaveSystem.DataStructures
{
    [Serializable]
    public class PlayerSaveData
    {
        public CharacterSaveData[] characterDatas;
        public InventoryData inventoryData;
        public QuestsData questsData;
        public BuildingsData buildingsData;

        public string sceneName;
    }
}

