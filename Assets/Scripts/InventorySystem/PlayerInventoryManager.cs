using System.Collections.Generic;

public class PlayerInventoryManager : Singletone<PlayerInventoryManager>
{
    public Inventory PlayerInventory { get; private set;}

    public Inventory levelCollectedItems { get; private set; }

    public void InitializeInventory()
    {
        PlayerInventory = new Inventory();
        levelCollectedItems = new Inventory();

        var items = PlayerDataManager.Data.inventoryData.items;

        foreach(var item in items)
        {
            PlayerInventory.AddItem(item.Id, item.Count);
        }
    }

    public void UseItem(UsableItem item, CharacterComponent byChracter)
    {
        if (PlayerInventory.HasItem(item.Id, 1))
        {
            item.Use(byChracter);
            PlayerInventory.RemoveItem(item.Id, 1);
            return;
        }
    }

    public void CollectLevelItems(Item item, int count)
    {
        PlayerInventory.AddItem(item.Id, count);
        levelCollectedItems.AddItem(item.Id, count);

        GameMessageScreen.ShowMessage($"Founded {item.Name} x{count}", item.icon);
    }

    public List<Item> GetWepons()
    {
        var weapons = new List<Item>();

        foreach (var item in PlayerInventory.items)
        {
            if (item.Item is Weapon)
            {
                weapons.Add(item.Item);
            }
        }

        return weapons;
    }

    public List<Item> GetArmors()
    {
        var armors = new List<Item>();

        foreach (var item in PlayerInventory.items)
        {
            if (item.Item is Armor)
            {
                armors.Add(item.Item);
            }
        }

        return armors;
    }

    public void Debug()
    {
        var str = "Player Inventory: \n";
        foreach (var item in PlayerInventory.items)
            str += $" - {item.Item.Name} x {item.Count}\n";

        UnityEngine.Debug.Log(str);
    }
}
