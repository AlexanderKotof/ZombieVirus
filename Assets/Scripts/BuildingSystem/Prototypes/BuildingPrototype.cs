using UnityEngine;

namespace BuildingSystem.Prototypes
{
    [CreateAssetMenu(menuName = "Game Entities/Building")]
    public class BuildingPrototype : ScriptableObject, IHasId
    {
        public int id;
        public int Id => id;

        public string Name;

        public string Description;

        public Sprite Icon;

        public int buildingTimeSec;

        public Inventory.InventoryItem[] requiredResources;
    }
}