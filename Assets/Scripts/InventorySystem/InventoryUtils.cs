using UnityEngine;

public static class InventoryUtils
{
    private static ItemsStorage _storage;

    public static Item GetItem(int id)
    {
        if (_storage == null)
            LoadStorage();

        return _storage.GetItem(id);
    }

    private static void LoadStorage()
    {
        _storage = Resources.Load<ItemsStorage>(nameof(ItemsStorage));
    }
}
