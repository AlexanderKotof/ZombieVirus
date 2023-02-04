using PlayerDataSystem.DataStructures;
using SaveSystem;
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
            SaveLoadSystem.SaveFile(Data);
        }
    }
}