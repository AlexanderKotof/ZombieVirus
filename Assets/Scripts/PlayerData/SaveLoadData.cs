using Features.CharactersFeature.Utils;
using System.IO;
using UnityEngine;

namespace SaveGameSystem
{
    public static partial class SaveLoadData
    {
        static string SaveFilename = "savedGame.save";
        public static string pathFormat =>
#if UNITY_ANDROID && !UNITY_EDITOR
              Application.persistentDataPath + "/" + SaveFilename;
#else
              Application.dataPath + "/" + SaveFilename;
#endif
        /// <summary>
        /// Write save file on disc
        /// </summary>
        /// <param name="playerData"></param>
        public static void SaveFile(PlayerData playerData)
        {
            var data = CreateSaveData(playerData);
            string dataToJson = JsonUtility.ToJson(data);
            File.WriteAllText(pathFormat, dataToJson);
        }

        private static PlayerSaveData CreateSaveData(PlayerData data)
        {
            var saveData = new PlayerSaveData();
            saveData.characterDatas = new CharacterSaveData[data.characterDatas.Length];

            for (int i = 0; i < data.characterDatas.Length; i++)
            {
                saveData.characterDatas[i] = new CharacterSaveData()
                {
                    currentExp = data.characterDatas[i].currentExp,
                    currentHealth = data.characterDatas[i].currentHealth,

                    prototypeId = data.characterDatas[i].prototype.Id,
                    armorId = data.characterDatas[i].armor.Id,
                    weaponId = data.characterDatas[i].weapon.Id,
                };
            }

            saveData.inventoryData = data.inventoryData;
            saveData.sceneName = data.currentScene;
            saveData.questsData = data.questsData;

            return saveData;
        }

        private static PlayerData CreatePlayerData(PlayerSaveData saveData)
        {
            var data = new PlayerData();

            data.characterDatas = new CharacterData[saveData.characterDatas.Length];

            for (int i = 0; i < saveData.characterDatas.Length; i++)
            {
                data.characterDatas[i] = new CharacterData()
                {
                    prototype = CharactersUtils.GetPrototype(saveData.characterDatas[i].prototypeId),
                    armor = InventoryUtils.GetItem(saveData.characterDatas[i].armorId) as Armor,
                    weapon = InventoryUtils.GetItem(saveData.characterDatas[i].weaponId) as Weapon,

                    currentExp = saveData.characterDatas[i].currentExp,
                    currentHealth = saveData.characterDatas[i].currentHealth,
                };
            }

            data.inventoryData = saveData.inventoryData;
            data.questsData = saveData.questsData;
            data.currentScene = saveData.sceneName;

            return data;
        }

        public static PlayerData Load()
        {
            if (File.Exists(pathFormat))
            {
                PlayerSaveData data = JsonUtility.FromJson<PlayerSaveData>(File.ReadAllText(pathFormat));
                if (data != null)
                    Debug.Log("Saved data loaded");
                return CreatePlayerData(data);
            }
            else
            {
                Debug.Log("Cannot load saved data");
                return null;
            }

        }

        public static void ClearPlayerData()
        {
            if (File.Exists(pathFormat))
            {
                File.Delete(pathFormat);
            }
        }

    }
}

