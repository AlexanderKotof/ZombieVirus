using Actions;
using System;
using UnityEngine;

namespace QuestSystem
{
    [CreateAssetMenu(menuName = "Game Entities/Quest")]
    public class Quest : ScriptableObject, IHasId
    {
        public int id;
        public int Id => id;

        public string Name;

        public string Description;

        public QuestStage[] Stages;

        public ExecuteAction[] OnCompletedActions;

        public Inventory.InventoryItem[] Reward;
    }

    [Serializable]
    public class QuestStage
    {
        public string Description;
    }
}
