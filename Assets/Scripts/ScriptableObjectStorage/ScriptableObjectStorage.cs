using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectStorage<T> : ScriptableObject where T : ScriptableObject, IHasId
{
    public T[] items;

    public Dictionary<int, T> _idsToItems;

    private void OnEnable()
    {
        _idsToItems = new Dictionary<int, T>(items.Length);
        foreach (var item in items)
        {
            _idsToItems.Add(item.Id, item);
        }
    }

    public T GetItem(int id)
    {
        if (_idsToItems.TryGetValue(id, out var item))
            return item;

        throw new System.Exception("Item not founded");
    }
}

public interface IHasId
{
    int Id { get; }
}
