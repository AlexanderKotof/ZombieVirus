using ScreenSystem.Components;

public class EquipmentButton : ButtonComponent
{
    public void SetItem(Item item)
    {
        SetImage(item.icon);
    }
}
