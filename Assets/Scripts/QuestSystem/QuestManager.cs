using PlayerDataSystem;
using QuestSystem.Utils;
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

            return true;
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

            if (data == null || data.completedStages.Contains(stage))
                return;

            data.completedStages.Add(stage);
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
        }
    }
}