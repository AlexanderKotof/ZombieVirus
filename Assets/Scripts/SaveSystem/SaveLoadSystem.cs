using PlayerDataSystem.DataStructures;
using SaveSystem.DataStructures;
using System.IO;
using UnityEngine;

namespace SaveSystem
{
    public static class SaveLoadSystem
    {
        private const string _saveFilename = "savedGame.save";
        private static string _dataPath =>
#if UNITY_ANDROID && !UNITY_EDITOR
              $"{Application.persistentDataPath}/{_saveFilename}";
#else
              $"{Application.dataPath}/{_saveFilename}";
#endif

        public static void SaveFile(PlayerData playerData)
        {
            //var data = Utils.SaveDataUtils.CreateSaveData(playerData);
            string dataToJson = JsonUtility.ToJson(playerData);
            File.WriteAllText(_dataPath, dataToJson);

            Debug.Log("Player data saved!");
        }

        public static PlayerData Load()
        {
            if (File.Exists(_dataPath))
            {
                PlayerData data = JsonUtility.FromJson<PlayerData>(File.ReadAllText(_dataPath));
                if (data != null)
                    Debug.Log("Saved data loaded");
                return data;
            }

            Debug.Log("Cannot load saved data");
            return null;
        }

        public static void ClearPlayerData()
        {
            if (!File.Exists(_dataPath))
                return;

            File.Delete(_dataPath);
            Debug.Log("Saved data cleared");
        }
    }
}

