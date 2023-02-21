using Features.CameraFeature.Systems;
using Features.CharactersFeature.Components;
using Features.GamePlayFeature.Data;
using Features.GamePlayFeature.Systems;
using Features.InputFeature.Controller;
using Features.InputFeature.Systems;
using Features.SkillsFeature.Prototypes;
using Features.SkillsFeature.Systems;
using FeatureSystem.Systems;
using ScreenSystem.Components;
using ScreenSystem.Screens;
using System;
using UI.Screens;
using UnityEngine;

public class GameHUDScreen : BaseScreen
{
    public TouchInputComponent touchInput;
    public ListComponent playerTeamList;
    public TargetHealthbarComponent targetHealthbar;
    public ListComponent damagePopupList;
    public ListComponent headshotPopupList;

    public ListComponent characterSkills;

    public CheckboxButtonComponent pauseButton;

    public TextComponent pauseMessage;

    private PlayerTeamSystem _teamSystem;

    private InputController _controller;

    private int _selectedCharacter = -1;

    private Camera gameCamera;

    protected override void OnShow()
    {
        base.OnShow();

        SetController();
        SetPlayerTeam();

        gameCamera = GameSystems.GetSystem<GameCameraSystem>().Camera.Camera;

        targetHealthbar.Hide();

        pauseButton.AddCallback(SetPause);

        touchInput.Tap += _controller.TapInput;
        touchInput.Draging += _controller.DragInput;

        DealDamageSystem.OnDamageTaken += OnDamageTaken;
    }

    private void OnDamageTaken(CharacterComponent target, float damage, bool critical)
    {
        if (!critical)
            damagePopupList.GetItemOrCreate<CharacterPopupComponent>((item, par) =>
            {
                item.ShowPopup(target, gameCamera);
                item.SetText(Mathf.RoundToInt(damage).ToString());
            });
        else
            AttackUtils_OnHeadshot(target, damage);
    }

    private void AttackUtils_OnHeadshot(CharacterComponent target, float damage)
    {
        headshotPopupList.GetItemOrCreate<CharacterPopupComponent>((item, par) =>
        {
            item.ShowPopup(target, gameCamera);
            item.SetText(Mathf.RoundToInt(damage).ToString());
        });
    }

    protected override void OnHide()
    {
        base.OnHide();

        pauseButton.RemoveCallback(_controller.SetPauseInput);

        touchInput.Tap -= _controller.TapInput;
        touchInput.Draging -= _controller.DragInput;

        DealDamageSystem.OnDamageTaken -= OnDamageTaken;
    }

    public void SetPause(bool value)
    {
        pauseMessage.ShowHide(value);
        _controller.SetPauseInput(value);
    }

    private void SetController()
    {
        _controller = GameSystems.GetSystem<PlayerInputSystem>().PlayerInput;
    }

    private void SetPlayerTeam()
    {
        _teamSystem = GameSystems.GetSystem<PlayerTeamSystem>();

        playerTeamList.SetItems<CharacterInfoComponent>(_teamSystem.Characters.Length, (item, par) =>
        {
            item.SetInfo(_teamSystem.Characters[par.index]);
            item.SetCallback(() => SwitchSelection(par.index));
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

        _controller.SwaitchCharactersInput(_selectedCharacter);

        var selectedCharacter = _teamSystem.GetSelectedCharacters()[0];
        if (selectedCharacter.target)
        {
            targetHealthbar.SetInfo(selectedCharacter.target);
            
        }
        else
        {
            targetHealthbar.Hide();
        }

        /*
        characterSkills.SetItems<SkilListItemComponent>(selectedCharacter.Data.prototype.skills.Length, (item, par) =>
        {
            var skill = selectedCharacter.Data.prototype.skills[par.index];
            item.SetInfo(skill, () => CastSkill(skill, selectedCharacter));
        });
        */

        for (int i = 0; i < playerTeamList.items.Count; i++)
        {
            var item = (CharacterInfoComponent)playerTeamList.items[i];
            item.SwitchSelection(_selectedCharacter == -1 || _selectedCharacter == i);
        }
    }

    private void CastSkill(SkillPrototype prototype, CharacterComponent caster)
    {
        var skillCastSystem = GameSystems.GetSystem<SkillCastSystem>();
        var target = caster.target;

        if (target == null)
        {
            return;
        }

        skillCastSystem.CastSkill(caster, prototype, target);
    }
}
