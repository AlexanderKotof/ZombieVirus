using ScreenSystem.Components;
using ScreenSystem.Screens;

public class HomeScreen : BaseScreen
{
    public CheckboxButtonComponent questsButton;
    public CheckboxButtonComponent buildingButton;
    public CheckboxButtonComponent teamButton;
    public CheckboxButtonComponent storeButton;

    public enum ScreenState
    {
        Quests,
        Building,
        Team,
        Store,
    }

    private ScreenState screenState;

    public WindowComponent questScreen;
    public WindowComponent buildingScreen;
    public WindowComponent teamScreen;
    public WindowComponent storeScreen;

    protected override void OnShow()
    {
        base.OnShow();

        questsButton.AddCallback(() => SwitchScreen(ScreenState.Quests));
        buildingButton.AddCallback(() => SwitchScreen(ScreenState.Building));
        teamButton.AddCallback(() => SwitchScreen(ScreenState.Team));
        storeButton.AddCallback(() => SwitchScreen(ScreenState.Store));

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
