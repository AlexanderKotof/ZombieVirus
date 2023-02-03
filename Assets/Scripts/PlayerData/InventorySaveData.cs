using System;

namespace SaveGameSystem
{
    [Serializable]
    public class InventorySaveData
    {
        public InventoryItem[] items;

        [Serializable]
        public class InventoryItem
        {
            public int Id;
            public int Count;

            public InventoryItem(int id, int count)
            {
                Id = id;
                Count = count;
            }
        }
    }
}

