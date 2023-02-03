using QuestSystem;
using Screens.HomeScreen.QuestWindow.Components;
using ScreenSystem.Components;
using System;

namespace Screens.HomeScreen.QuestWindow
{
    public class QuestWindowComponent : WindowComponent
    {
        public ListComponent questsList;

        public TextComponent selectedQuestName;
        public TextComponent selectedQuestDescription;

        public ListComponent selectedQuestStages;

        public ListComponent selectedQuestReward;

        public ButtonComponent completeQuestButton;

        private QuestManager.QuestData _selectedQuest;

        protected override void OnShow()
        {
            base.OnShow();

            SetQuestList();

            completeQuestButton.SetCallback(CompleteQuest);
        }

        private void CompleteQuest()
        {
            if (QuestManager.Instance.CompleteQuest(_selectedQuest.quest))
            {
                // show reward screen
                // update window
            }
        }

        private void SetQuestList()
        {
            var quests = QuestManager.Instance.currentQuests;
            quests.AddRange(QuestManager.Instance.completedQuests);

            questsList.SetItems<QuestListItemComponent>(quests.Count, (item, par) =>
            {
                item.SetQuest(quests[par.index].quest, quests[par.index].isCompleted);
                item.SetCallback(() => SelectQuest(quests[par.index]));
            });

            SelectQuest(quests[0]);
        }

        private void SelectQuest(QuestManager.QuestData questData)
        {
            _selectedQuest = questData;

            var quest = questData.quest;

            selectedQuestName.SetText(quest.Name);
            selectedQuestDescription.SetText(quest.Description);

            selectedQuestStages.SetItems<QuestStageListItemComponent>(quest.Stages.Length, (item, par) =>
            {
                item.SetInfo(quest.Stages[par.index].Description, questData.completedStages.Contains(par.index));
            });

            //show reward
            //selectedQuestReward.

            completeQuestButton.SetInteractable(QuestManager.Instance.CanComplete(quest));
        }
    }
}