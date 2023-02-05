using ScreenSystem.Components;

namespace UI.SharedComponents
{
    public class InventoryListItem : WindowComponent
    {
        public ImageComponent itemIcon;
        public TextComponent countText;

        public void SetInfo(Item item, int count)
        {
            itemIcon.SetImage(item.icon);

            if (count > 1)
            {
                countText.Show();
                countText.SetText(count.ToString());
            }
            else
            {
                countText.Hide();
            }
        }

        public void SetInfo(int itemId, int count)
        {
            var item = InventoryUtils.GetItem(itemId);
            SetInfo(item, count);
        }

        public void SetInfo(Inventory.InventoryItem inventoryItem)
        {
            SetInfo(inventoryItem.Item, inventoryItem.Count);
        }
    }
}