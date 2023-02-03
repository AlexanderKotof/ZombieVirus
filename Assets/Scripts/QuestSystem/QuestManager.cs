using QuestSystem.Utils;
using System.Collections;
using System.Collections.Generic;

namespace QuestSystem
{
    public class QuestManager : Singletone<QuestManager>
    {
        public List<QuestData> currentQuests;
        public List<QuestData> completedQuests;

        public class QuestData
        {
            public Quest quest;
            public List<int> completedStages;
            public bool isCompleted;
        }

        public void Initialize()
        {
            var savedData = PlayerDataManager.Data.questsData;

            currentQuests = new List<QuestData>(savedData.curentQuestsData.Length);
            foreach (var data in savedData.curentQuestsData)
            {
                var questData = new QuestData()
                {
                    quest = QuestUtils.GetQuest(data.questId),
                    completedStages = new List<int>(data.completedStages),
                    isCompleted = false,
                };
                currentQuests.Add(questData);
            }

            completedQuests = new List<QuestData>(savedData.complitedQuestsData.Length);
            foreach (var data in savedData.complitedQuestsData)
            {
                var questData = new QuestData()
                {
                    quest = QuestUtils.GetQuest(data.questId),
                    completedStages = new List<int>(data.completedStages),
                    isCompleted = true,
                };
                completedQuests.Add(questData);
            }
        }

        public QuestData GetQuestData(Quest quest)
        {
            foreach (var data in currentQuests)
            {
                if (data.quest == quest)
                    return data;
            }

            return null;
        }

        public bool CompleteQuest(Quest quest)
        {
            if (!CanComplete(quest))
                return false;

            var data = GetQuestData(quest);

            data.isCompleted = true;

            ReceiveRewards(quest);
            AddNextQuests(quest);

            currentQuests.Remove(data);
            completedQuests.Add(data);

            UpdateQuestsData();

            return true;
        }

        private void UpdateQuestsData()
        {
            var questData = PlayerDataManager.Data.questsData;

            questData.complitedQuestsData = new SaveGameSystem.QuestsSaveData.QuestData[completedQuests.Count];
            for (int i = 0; i < completedQuests.Count; i++)
            {
                QuestData complQ = (QuestData)completedQuests[i];
                questData.complitedQuestsData[i] = new SaveGameSystem.QuestsSaveData.QuestData()
                {
                    questId = complQ.quest.id,
                    completedStages = new int[0],
                };
            }

            questData.curentQuestsData = new SaveGameSystem.QuestsSaveData.QuestData[currentQuests.Count];
            for (int i = 0; i < currentQuests.Count; i++)
            {
                QuestData currQ = (QuestData)currentQuests[i];
                questData.curentQuestsData[i] = new SaveGameSystem.QuestsSaveData.QuestData()
                {
                    questId = currQ.quest.id,
                    completedStages = currQ.completedStages.ToArray(),
                };
            }
        }

        public bool CanComplete(Quest quest)
        {
            var data = GetQuestData(quest);

            if (data == null)
                return false;

            if (quest.Stages.Length > data.completedStages.Count)
                return false;

            if (data.isCompleted)
                return false;

            return true;
        }

        public void CompleteStage(Quest quest, int stage)
        {
            var data = GetQuestData(quest);

            if (data.completedStages.Contains(stage))
                return;

            data.completedStages.Add(stage);

            UpdateQuestsData();
        }

        private void ReceiveRewards(Quest quest)
        {
            foreach (var item in quest.Reward)
            {

            }
        }

        private void AddNextQuests(Quest quest)
        {
            if (quest.NextQuests != null && quest.NextQuests.Length > 0)
            {
                foreach (var q in quest.NextQuests)
                {
                    currentQuests.Add(new QuestData()
                    {
                        quest = q,
                        completedStages = new List<int>(),
                    });
                }
            }

            UpdateQuestsData();
        }
    }
}