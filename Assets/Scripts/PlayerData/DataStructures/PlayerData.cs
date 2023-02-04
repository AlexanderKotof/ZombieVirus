using SaveSystem.DataStructures;
using System;

namespace PlayerDataSystem.DataStructures
{
    [Serializable]
    public class PlayerData
    {
        public CharacterData[] characterDatas;

        public InventorySaveData inventoryData;

        public QuestsSaveData questsData;

        public string currentScene;
    }
}