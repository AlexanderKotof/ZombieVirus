using UnityEngine;

namespace Actions
{
    [CreateAssetMenu(menuName = "Game Entities/Actions/Add New Quests")]
    public class AddNewQuestsAction : ExecuteAction
    {
        public QuestSystem.Quest[] quests;

        public override void Execute()
        {
            QuestSystem.QuestManager.Instance.AddNewQuests(quests);
        }
    }
}
