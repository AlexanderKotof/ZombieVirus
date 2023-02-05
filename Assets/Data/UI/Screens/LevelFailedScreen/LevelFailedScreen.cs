using ScreenSystem.Components;
using ScreenSystem.Screens;
using System;

public class LevelFailedScreen : BaseScreen
{
    public TextComponent levelEndedMessage;

    public ButtonComponent restartButton;
    public ButtonComponent returnHomeButton;

    private Action _returnHomeCallback;
    private Action _restartCallback;

    public void SetButtonsCallback(Action returnHomeCallback, Action restartCallback)
    {
        _returnHomeCallback = returnHomeCallback;
        _restartCallback = restartCallback;
    }

    protected override void OnShow()
    {
        base.OnShow();

        restartButton.SetCallback(OnRestartButtonClick);
        returnHomeButton.SetCallback(OnReturnButtonClick);
    }

    private void OnRestartButtonClick()
    {
        Hide();
        _restartCallback?.Invoke();
    }

    private void OnReturnButtonClick()
    {
        Hide();
        _returnHomeCallback?.Invoke();
    }
}
