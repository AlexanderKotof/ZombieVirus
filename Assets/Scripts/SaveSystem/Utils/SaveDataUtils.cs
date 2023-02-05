﻿using Features.CharactersFeature.Utils;
using PlayerDataSystem.DataStructures;
using QuestSystem;
using SaveSystem.DataStructures;
using System;
using static SaveSystem.DataStructures.QuestsSaveData;

namespace SaveSystem.Utils
{
    public static class SaveDataUtils
    {
        public static PlayerData CreatePlayerData(PlayerSaveData saveData)
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
            data.buildingsData = saveData.buildingsData;

            data.currentScene = saveData.sceneName;

            return data;
        }

        public static PlayerSaveData CreateSaveData(PlayerData data)
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

            saveData.inventoryData = CreateInventorySaveData();

            saveData.sceneName = data.currentScene;

            saveData.questsData = CreateQuestSave();
            saveData.buildingsData = CreateBuildingsData();

            return saveData;
        }

        private static InventorySaveData CreateInventorySaveData()
        { 
            var data = new InventorySaveData();

            var manager = PlayerInventoryManager.Instance;
            var items = manager.PlayerInventory.GetItems();

            data.items = new InventorySaveData.InventoryItem[items.Length];

            for (int i = 0; i < items.Length; i++)
            {
                data.items[i] = new InventorySaveData.InventoryItem(items[i].Item.Id, items[i].Count);
            }

            return data;
        }

        private static QuestsSaveData CreateQuestSave()
        {
            var questData = new QuestsSaveData();

            var completedQuests = QuestManager.Instance.completedQuests;
            questData.complitedQuestsData = new QuestData[completedQuests.Count];
            for (int i = 0; i < completedQuests.Count; i++)
            {
                var complQ = completedQuests[i];
                questData.complitedQuestsData[i] = new QuestData()
                {
                    questId = complQ.quest.id,
                    completedStages = new int[0],
                };
            }

            var currentQuests = QuestManager.Instance.currentQuests;
            questData.curentQuestsData = new QuestData[currentQuests.Count];
            for (int i = 0; i < currentQuests.Count; i++)
            {
                var currQ = currentQuests[i];
                questData.curentQuestsData[i] = new QuestData()
                {
                    questId = currQ.quest.id,
                    completedStages = currQ.completedStages.ToArray(),
                };
            }

            return questData;
        }

        private static BuildingsSaveData CreateBuildingsData()
        {
            var buildingSave = new BuildingsSaveData();
            var manager = BuildingSystem.BuildingManager.Instance;

            buildingSave.buildedIds = new int[manager.builded.Count];
            for (int i = 0; i < manager.builded.Count; i++)
            {
                buildingSave.buildedIds[i] = manager.builded[i].id;
            }

            buildingSave.readyToBuildIds = new int[manager.readyForBuild.Count];
            for (int i = 0; i < manager.readyForBuild.Count; i++)
            {
                buildingSave.readyToBuildIds[i] = manager.readyForBuild[i].id;
            }

            buildingSave.buldingProgressId = manager.nowBuilds ? manager.nowBuilds.id : -1;
            buildingSave.startedAt = manager.startedAt;

            return buildingSave;
        }
    }
}

