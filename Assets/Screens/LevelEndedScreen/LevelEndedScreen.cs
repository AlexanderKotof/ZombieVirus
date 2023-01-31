using ScreenSystem.Components;
using ScreenSystem.Screens;
using System;

public class LevelEndedScreen : BaseScreen
{
    public ListComponent collectedItemsList;

    public TextComponent levelEndedMessage;
    public ButtonComponent continueButton;

    private Action _callback;

    public void SetButtonCallback(Action callback)
    {
        _callback = callback;
    }

    protected override void OnShow()
    {
        base.OnShow();

        var foundedItems = PlayerInventoryManager.Instance.levelCollectedItems;
        collectedItemsList.SetItems<RewardListItem>(foundedItems.items.Count, (item, par) =>
        {
            var invItem = foundedItems.items[par.index];
            item.SetInfo(invItem.Item, invItem.Count);
        });

        continueButton.SetCallback(OnContinueButtonClick);
    }

    private void OnContinueButtonClick()
    {
        Hide();
        _callback?.Invoke();
    }
}
