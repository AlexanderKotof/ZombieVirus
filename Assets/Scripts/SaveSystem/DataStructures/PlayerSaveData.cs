using SaveSystem.Utils;
using System;

namespace SaveSystem.DataStructures
{
    [Serializable]
    public class PlayerSaveData
    {
        public CharacterSaveData[] characterDatas;
        public InventorySaveData inventoryData;
        public QuestsSaveData questsData;

        public string sceneName;
    }
}

