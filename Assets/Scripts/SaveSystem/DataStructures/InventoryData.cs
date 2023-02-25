using System;

namespace SaveSystem.DataStructures
{
    [Serializable]
    public class InventoryData
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

