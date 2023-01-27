using System.IO;
using UnityEngine;

namespace SaveGameSystem
{
    public static class SaveLoadData
    {
        static string SaveFilename = "savedGame1.json";
        public static string pathFormat = null;

        /// <summary>
        /// Write save file on disc
        /// </summary>
        /// <param name="saveData"></param>
        public static void SaveFile(PlayerData saveData)
        {
            if (pathFormat == null)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                pathFormat =   Application.persistentDataPath + "/" + SaveFilename;
#else
                pathFormat = Application.dataPath + "/" + SaveFilename;
#endif
            }

            string dataToJson = JsonUtility.ToJson(saveData);
            File.WriteAllText(pathFormat, dataToJson);
        }

        public static PlayerData Load()
        {
            if (pathFormat == null)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                pathFormat =   Application.persistentDataPath + "/" + SaveFilename;
#else
                pathFormat = Application.dataPath + "/" + SaveFilename;
#endif
            }

            if (File.Exists(pathFormat))
            {
                PlayerData data = JsonUtility.FromJson<PlayerData>(File.ReadAllText(pathFormat));
                if (data != null)
                    Debug.Log("Saved data loaded");
                return data;
            }
            else
            {
                Debug.Log("Cannot load saved data");
                return null;
            }

        }

    }
}

