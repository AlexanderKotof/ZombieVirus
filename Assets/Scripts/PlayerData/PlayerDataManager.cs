using PlayerDataSystem.DataStructures;
using SaveSystem;
using SaveSystem.Utils;
using UnityEngine;

namespace PlayerDataSystem
{
    public class PlayerDataManager : MonoBehaviour
    {
        public PlayerData defaultData;
        public static PlayerData Data { get; private set; }

        private static PlayerDataManager _instance;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            _instance = this;
        }

        public static PlayerData LoadOrCreateNewData()
        {
            Data = SaveLoadSystem.Load();

            if (Data != null)
                return Data;

            Data = _instance.defaultData;

            return Data;
        }

        public static void ClearData()
        {
            SaveLoadSystem.ClearPlayerData();
        }

        public static void SaveData()
        {
            UpdateSaveData();
            SaveLoadSystem.SaveFile(Data);
        }

        private static void UpdateSaveData()
        {
            Data.inventoryData = PlayerDataUtils.CreateInventorySaveData();
            Data.questsData = PlayerDataUtils.CreateQuestSave();

            // now building data is not used
            // Data.buildingsData = SaveDataUtils.CreateBuildingsData();
        }
    }
}