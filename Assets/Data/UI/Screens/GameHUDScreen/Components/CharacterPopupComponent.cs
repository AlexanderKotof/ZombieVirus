using Features.CharactersFeature.Components;
using ScreenSystem.Components;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPopupComponent : WindowComponent
{
    private Camera _screenCamera;
    private CharacterComponent _target;

    public CanvasGroup group;
    public TMP_Text popupText;
    public float upMovingSpeed = 100f;
    public float alphaChangeSpeed = 1f;

    public Vector3 startOffset = Vector3.up * 100;

    private float _startAlpha = 1;

    protected override void OnInit()
    {
        base.OnInit();
    }

    public void ShowPopup(CharacterComponent target, Camera screenCamera)
    {
        _target = target;
        _screenCamera = screenCamera;

        Show();

        StartCoroutine(ShowPopup());
    }

    public void SetText(string text)
    {
        popupText.text = text;
    }

    private IEnumerator ShowPopup()
    {
        group.alpha = _startAlpha;
        Vector3 offset = this.startOffset;

        var targetPosition = _target.Position;

        while (group.alpha > 0)
        {
            offset += Vector3.up * upMovingSpeed * Time.deltaTime;
            RectTransform.Reposition(_screenCamera, targetPosition, offset);
            group.alpha -= Time.deltaTime * alphaChangeSpeed;
            yield return null;
        }

        Hide();
    }
}
