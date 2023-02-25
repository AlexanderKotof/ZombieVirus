using Features.CharactersFeature.Utils;
using PlayerDataSystem.DataStructures;
using QuestSystem;
using SaveSystem.DataStructures;
using System;
using static SaveSystem.DataStructures.QuestsData;

namespace SaveSystem.Utils
{
    public static class PlayerDataUtils
    {
        public static InventoryData CreateInventorySaveData()
        { 
            var data = new InventoryData();

            var manager = PlayerInventoryManager.Instance;
            var items = manager.PlayerInventory.GetItems();

            data.items = new InventoryData.InventoryItem[items.Length];

            for (int i = 0; i < items.Length; i++)
            {
                data.items[i] = new InventoryData.InventoryItem(items[i].Item.Id, items[i].Count);
            }

            return data;
        }

        public static QuestsData CreateQuestSave()
        {
            var questData = new QuestsData();

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

        public static BuildingsData CreateBuildingsData()
        {
            var buildingSave = new BuildingsData();
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

