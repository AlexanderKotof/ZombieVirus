using ScreenSystem.Components;
using UnityEngine;
using UnityEngine.UI;

public class ImageComponent : WindowComponent
{
    public Image image;

    private void OnValidate()
    {
        image = GetComponent<Image>();
    }

    public void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
    }
}