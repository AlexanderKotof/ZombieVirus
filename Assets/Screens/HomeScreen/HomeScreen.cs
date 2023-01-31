using ScreenSystem.Components;
using ScreenSystem.Screens;
using UnityEngine;

public class HomeScreen : BaseScreen
{
    public CheckboxButtonComponent questsButton;
    public CheckboxButtonComponent buildingButton;
    public CheckboxButtonComponent teamButton;
    public CheckboxButtonComponent storeButton;

    public ButtonComponent gotoLevelButton;

    public enum ScreenState
    {
        Quests,
        Building,
        Team,
        Store,
    }

    private ScreenState screenState;
    [Space]
    public WindowComponent questScreen;
    public WindowComponent buildingScreen;
    public WindowComponent teamScreen;
    public WindowComponent storeScreen;

    protected override void OnShow()
    {
        base.OnShow();

        questsButton.SetCallback(() => SwitchScreen(ScreenState.Quests));
        buildingButton.SetCallback(() => SwitchScreen(ScreenState.Building));
        teamButton.SetCallback(() => SwitchScreen(ScreenState.Team));
        storeButton.SetCallback(() => SwitchScreen(ScreenState.Store));

        gotoLevelButton.SetCallback(GameManager.Instance.GoToGameScene);

        SwitchScreen(ScreenState.Quests);
    }

    private void SwitchScreen(ScreenState state)
    {
        screenState = state;

        questsButton.SetCheckedState(state == ScreenState.Quests, false);
        buildingButton.SetCheckedState(state == ScreenState.Building, false);
        teamButton.SetCheckedState(state == ScreenState.Team, false);
        storeButton.SetCheckedState(state == ScreenState.Store, false);

        questScreen.ShowHide(state == ScreenState.Quests);
        buildingScreen.ShowHide(state == ScreenState.Building);
        teamScreen.ShowHide(state == ScreenState.Team);
        storeScreen.ShowHide(state == ScreenState.Store);
    }

    protected override void OnHide()
    {
        base.OnHide();

        questsButton.RemoveAllCallbacks();
        buildingButton.RemoveAllCallbacks();
        teamButton.RemoveAllCallbacks();
        storeButton.RemoveAllCallbacks();
    }
}
