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
            var data = Utils.SaveDataUtils.CreateSaveData(playerData);
            string dataToJson = JsonUtility.ToJson(data);
            File.WriteAllText(_dataPath, dataToJson);

            Debug.Log("Player data saved!");
        }

        public static PlayerData Load()
        {
            if (File.Exists(_dataPath))
            {
                PlayerSaveData data = JsonUtility.FromJson<PlayerSaveData>(File.ReadAllText(_dataPath));
                if (data != null)
                    Debug.Log("Saved data loaded");
                return Utils.SaveDataUtils.CreatePlayerData(data);
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

