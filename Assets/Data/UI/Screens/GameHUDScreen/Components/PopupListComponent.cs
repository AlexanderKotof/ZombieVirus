using ScreenSystem.Components;
using System;
using UnityEngine;
using static ScreenSystem.Components.ListComponent;

public class PopupListComponent : MonoBehaviour
{
    public ListComponent list;

    public int queue;

    public void AddItem(Action<WindowComponent, ListParameters> onComplite)
    {
        var index = list.items.FindIndex((x) => { return !x.gameObject.activeSelf; });


            list.AddItem<WindowComponent>(onComplite);

    }
}
