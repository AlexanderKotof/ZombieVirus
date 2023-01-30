using ScreenSystem.Components;

public class RewardListItem : WindowComponent
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
}
