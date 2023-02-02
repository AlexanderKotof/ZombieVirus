using System;
using System.Collections.Generic;
using System.Linq;

public class Inventory
{
    public IEnumerable<InventoryItem> Items => _idsToItems.Values;

    private Dictionary<int, InventoryItem> _idsToItems = new Dictionary<int, InventoryItem>();

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
        if (_idsToItems.TryGetValue(item.Id, out var invItem))
        {
            invItem.Count += count;
        }
        else
        {
            _idsToItems.Add(item.Id, new InventoryItem(item, count));
        }
    }

    public bool HasItem(int id, int count = 1)
    {
        if (_idsToItems.TryGetValue(id, out var invItem))
        {
            return invItem.Count >= count;
        }

        return false;
    }

    public bool HasItem(Item item, int count = 1)
    {
        return HasItem(item.Id, count);
    }

    public bool RemoveItem(int id, int count = 1)
    {
        if (_idsToItems.TryGetValue(id, out var invItem))
        {
            if (invItem.Count > count)
            {
                invItem.Count -= count;
                return true;
            }
            else if (invItem.Count == count)
            {
                _idsToItems.Remove(id);
                return true;
            }
        }

        return false;
    }

    public bool RemoveItem(Item item, int count = 1)
    {
        return RemoveItem(item.Id, count);
    }

    public InventoryItem[] GetItems()
    {
        return Items.ToArray();
    }
}
