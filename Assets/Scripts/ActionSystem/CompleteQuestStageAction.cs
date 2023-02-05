using UnityEngine;

namespace Actions
{
    [CreateAssetMenu(menuName = "Game Entities/Actions/Complete Quest Stage")]
    public class CompleteQuestStageAction : ExecuteAction
    {
        public QuestSystem.Quest quest;
        public int stage;

        public override void Execute()
        {
            QuestSystem.QuestManager.Instance.CompleteStage(quest, stage);
        }
    }
}
