using Features.CharactersFeature.Components;
using Features.InteractionFeature.Components;
using QuestSystem;
using UnityEngine;

namespace Features.InteractionFeature.Actions
{
    [CreateAssetMenu(menuName = "Game Entities/Interactions/Complete Quest Stage")]
    public class CompleteQuestStageAction : InteractionAction
    {
        public Quest attachedQuest;
        public int completeStageIndex;

        public override bool CanInteract(CharacterComponent character)
        {
            return true;
        }

        public override void Interact(CharacterComponent character, InteractionComponent interaction)
        {
            QuestManager.Instance.CompleteStage(attachedQuest, completeStageIndex);
        }
    }
}