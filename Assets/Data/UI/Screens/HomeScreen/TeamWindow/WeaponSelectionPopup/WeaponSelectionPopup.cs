using ScreenSystem.Components;
using ScreenSystem.Screens;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectionPopup : BaseScreen
{
    public ListComponent weaponsList;
    public ButtonComponent backgroundCloseButton;

    protected override void OnInit()
    {
        base.OnInit();
        backgroundCloseButton.SetCallback(Hide);
    }

    public void ShowPopup(Vector3 position, List<Item> items, Action<Item> onSelected)
    {
        weaponsList.RectTransform.position = position;
        weaponsList.SetItems<ButtonComponent>(items.Count, (item, par) =>
        {
            var weaponItem = items[par.index];
            item.SetImage(weaponItem.icon);
            item.SetCallback(() => 
            {
                Hide();
                onSelected?.Invoke(weaponItem);
            });
        });
    }

    public static void ShowWeaponsPopup(Vector3 position, List<Item> items, Action<Item> onSelected)
    {
        ScreenSystem.ScreensManager.ShowScreen<WeaponSelectionPopup>().ShowPopup(position, items, onSelected);
    }
}
