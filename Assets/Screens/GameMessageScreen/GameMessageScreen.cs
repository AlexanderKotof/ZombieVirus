using ScreenSystem.Components;
using ScreenSystem.Screens;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMessageScreen : BaseScreen
{
    private static GameMessageScreen _instance;

    public TextComponent messageText;

    public ImageComponent image;

    public CanvasGroup messageGroup;

    public float fadeSpeed = 1;

    public float showTime = 1;

    private List<(Sprite icon, string message)> _messages = new List<(Sprite, string)>();

    private Coroutine _showingCoroutine;

    public static void ShowMessage(string message, Sprite sprite = null)
    {
        if (!_instance)
        {
            _instance = ScreenSystem.ScreensManager.ShowScreen<GameMessageScreen>();
        }

        _instance._messages.Add((sprite, message));
    }

    private void ShowMessageInternal(Sprite sprite, string message)
    {
        messageText.SetText(message);

        if (sprite == null)
        {
            image.Hide();
        }
        else
        {
            image.Show();
            image.SetImage(sprite);
        }

        _showingCoroutine = StartCoroutine(FadeInOut());
    }

    private void Update()
    {
        if (_showingCoroutine != null)
            return;

        if (_messages.Count == 0)
            return;

        ShowMessageInternal(_messages[0].icon, _messages[0].message);
        _messages.RemoveAt(0);
    }

    private IEnumerator FadeInOut()
    {
        messageGroup.alpha = 0;

        while(messageGroup.alpha < 1)
        {
            messageGroup.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }

        yield return new WaitForSeconds(showTime);

        while (messageGroup.alpha > 0)
        {
            messageGroup.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }

        _showingCoroutine = null;
    }
}
