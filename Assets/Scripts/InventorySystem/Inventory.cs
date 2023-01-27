using System;
using System.Collections.Generic;

public class Inventory
{
    public List<InventoryItem> items = new List<InventoryItem>();

    private Dictionary<int, int> _idsToIndexes = new Dictionary<int, int>();

    public void AddItem(int id, int count = 1)
    {
        if (_idsToIndexes.TryGetValue(id, out var index))
        {
            items[index].Count += count;
        }
        else
        {
            items.Add(new InventoryItem(id, count));
            _idsToIndexes.Add(id, items.Count - 1);
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
}
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
