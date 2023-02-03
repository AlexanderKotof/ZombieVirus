using System;

namespace SaveGameSystem
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

