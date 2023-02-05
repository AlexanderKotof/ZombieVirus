using PlayerDataSystem;
using QuestSystem.Utils;
using System;
using System.Collections.Generic;

namespace QuestSystem
{
    public class QuestManager : Singletone<QuestManager>
    {
        public List<QuestData> currentQuests;
        public List<QuestData> completedQuests;

        public event Action QuestsUpdated;

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

            foreach (var action in quest.OnCompletedActions)
                action.Execute();

            currentQuests.Remove(data);
            completedQuests.Add(data);

            QuestsUpdated?.Invoke();

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

            QuestsUpdated?.Invoke();
        }

        private void ReceiveRewards(Quest quest)
        {
            foreach (var item in quest.Reward)
            {

            }
        }

        public void AddNewQuests(Quest[] quests)
        {
            foreach (var q in quests)
            {
                currentQuests.Add(new QuestData()
                {
                    quest = q,
                    completedStages = new List<int>(),
                });
            }

            QuestsUpdated?.Invoke();
        }
    }
}