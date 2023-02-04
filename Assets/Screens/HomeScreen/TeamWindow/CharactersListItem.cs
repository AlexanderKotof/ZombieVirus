using PlayerDataSystem.DataStructures;
using ScreenSystem.Components;
using UnityEngine;
using UnityEngine.UI;

public class CharactersListItem : ButtonComponent
{
    public Image selection;

    public Sprite EmptyIcon;

    private CharacterData _data;

    public void SetInfo(CharacterData data)
    {
        _data = data;

        if (_data == null)
        {
            ShowEmpty();
            return;
        }

        image.enabled = true;
        SetImage(data.prototype.metaData.characterIcon);
    }

    private void ShowEmpty()
    {
        image.enabled = false;
    }

    public void Select(bool value)
    {
        selection.enabled = value;
    }
}
