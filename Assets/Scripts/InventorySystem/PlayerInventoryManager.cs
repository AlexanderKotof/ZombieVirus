public class PlayerInventoryManager : Singletone<PlayerInventoryManager>
{
    public Inventory PlayerInventory { get; private set;}

    public void InitializeInventory()
    {
        PlayerInventory = new Inventory();

        var items = PlayerDataManager.Data.inventoryData.items;

        foreach(var item in items)
        {
            PlayerInventory.AddItem(item.Id, item.Count);
        }
    }

}
