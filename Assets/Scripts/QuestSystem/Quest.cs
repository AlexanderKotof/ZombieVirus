using UnityEngine;

[CreateAssetMenu(menuName = "Game Entities/Quest")]
public class Quest : ScriptableObject, IHasId
{
    public int id;
    public int Id => id;

    public string Name;

    public string Description;

    public Quest[] nextQuests;

    public Inventory.InventoryItem[] reward;
}

