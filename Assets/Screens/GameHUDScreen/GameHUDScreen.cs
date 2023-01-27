using FeatureSystem.Systems;
using ScreenSystem.Components;
using ScreenSystem.Screens;
using UI.Screens;

public class GameHUDScreen : BaseScreen
{
    public TouchInputComponent touchInput;
    public ListComponent playerTeamList;
    public TargetHealthbarComponent targetHealthbar;

    private PlayerTeam playerTeam => _teamSystem.team;
    private PlayerTeamSystem _teamSystem;

    private InputController _controller;

    private int _selectedCharacter = -1;

    public void SetController()
    {
        _controller = GameSystems.GetSystem<PlayerInputSystem>().PlayerInput;
    }

    public void SetPlayerTeam()
    {
        _teamSystem = GameSystems.GetSystem<PlayerTeamSystem>();

        playerTeamList.SetItems<CharacterInfoComponent>(playerTeam.characters.Length, (item, par) =>
        {
            item.SetInfo(playerTeam.characters[par.index]);
            item.AddCallback(() => SwitchSelection(par.index));
        });

        SwitchSelection(_selectedCharacter);
    }

    public void SetTarget(CharacterComponent target)
    {
        if (target == null)
        {
            targetHealthbar.Hide();
        }
        else
        {
            targetHealthbar.SetInfo(target);
        }
    }

    private void SwitchSelection(int index)
    {
        if (index == _selectedCharacter)
        {
            _selectedCharacter = -1;
        }
        else
        {
            _selectedCharacter = index;
        }

        _controller.SwitchCharacterSelection?.Invoke(_selectedCharacter);

        var selectedCharacter = _teamSystem.GetSelectedCharacters()[0];
        if (selectedCharacter.CurrentCommand != null && selectedCharacter.CurrentCommand is AttackCommand attackCommand)
        {
            targetHealthbar.SetInfo(attackCommand.Target);
        }
        else
        {
            targetHealthbar.Hide();
        }

        for (int i = 0; i < playerTeamList.items.Count; i++)
        {
            var item = (CharacterInfoComponent)playerTeamList.items[i];
            item.SwitchSelection(_selectedCharacter == -1 || _selectedCharacter == i);
        }
    }

    protected override void OnShow()
    {
        base.OnShow();

        SetController();
        SetPlayerTeam();

        targetHealthbar.Hide();

        touchInput.Tap += _controller.OnTap;
        touchInput.Draging += _controller.OnDrag;
    }

    protected override void OnHide()
    {
        base.OnHide();

        touchInput.Tap -= _controller.OnTap;
        touchInput.Draging -= _controller.OnDrag;
    }
}
