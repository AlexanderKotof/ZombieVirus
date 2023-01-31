using System;
using System.Collections.Generic;

public class Inventory
{
    public List<InventoryItem> items = new List<InventoryItem>();

    private Dictionary<int, int> _idsToIndexes = new Dictionary<int, int>();

    [Serializable]
    public class InventoryItem
    {
        public Item Item;
        public int Count;

        public InventoryItem(Item item, int count)
        {
            Item = item;
            Count = count;
        }
    }

    public void AddItem(int id, int count = 1)
    {
        var item = InventoryUtils.GetItem(id);
        AddItem(item, count);
    }

    public void AddItem(Item item, int count = 1)
    {
        if (_idsToIndexes.TryGetValue(item.Id, out var index))
        {
            items[index].Count += count;
        }
        else
        {
            items.Add(new InventoryItem(item, count));
            _idsToIndexes.Add(item.Id, items.Count - 1);
        }
    }

    public bool HasItem(int id, int count = 1)
    {
        if (_idsToIndexes.TryGetValue(id, out var index))
        {
            return items[index].Count >= count;
        }

        return false;
    }

    public bool RemoveItem(int id, int count = 1)
    {
        if (_idsToIndexes.TryGetValue(id, out var index))
        {
            if (items[index].Count > count)
            {
                items[index].Count -= count;
                return true;
            }
            else if (items[index].Count == count)
            {
                items.RemoveAt(index);
                _idsToIndexes.Remove(id);
                return true;
            }
        }

        return false;
    }

    public bool RemoveItem(Item item, int count = 1)
    {
        return RemoveItem(item.Id, count);
    }
}
