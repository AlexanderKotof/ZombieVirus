using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Entities/Quest")]
public class Quest : ScriptableObject, IHasId
{
    public int id;
    public int Id => id;

    public string Name;

    public string Description;

    public QuestStage[] Stages;

    public Quest[] NextQuests;

    public Inventory.InventoryItem[] Reward;
}

[Serializable]
public class QuestStage
{
    public string Description;
}

