using ScreenSystem.Components;

public class EquipmentButton : ButtonComponent
{
    public void SetItem(Item item)
    {
        if (item)
        {
            image.enabled = true;
            SetImage(item.icon);
        }
        else
            image.enabled = false;
    }
}
