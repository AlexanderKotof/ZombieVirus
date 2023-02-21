using UnityEngine;

public static class UIUtils
{
    public static void Reposition(this RectTransform rectTransform, Camera screenCamera, Vector3 scenePosition, Vector3 screenOffset)
    {
        var position = screenCamera.WorldToScreenPoint(scenePosition);
        rectTransform.position = position + screenOffset;
    }
}
