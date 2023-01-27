using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerTeam team;
    public CameraRigComponent cameraRig;

    public EnemyCommandSystem commandSystem;

    public Transform playerSpawnPoint;

    private InputController _input;

    public static GameManager instance;

    void Start()
    {
        instance = this;

        _input = new InputController();

        team.InstatiateCharacters(playerSpawnPoint);

        var screen = ScreenSystem.ScreensManager.ShowScreen<GameHUDScreen>();
        screen.SetController(_input);
        screen.SetPlayerTeam(team);

        _input.OnDrag += MoveCamera;
        _input.OnTap += CommandPlayerCharacters;
        _input.SwitchCharacterSelection += SwitchSelection;

    }

    private void SwitchSelection(int obj)
    {
        team.selectedCharacters = obj;
    }

    private void OnDestroy()
    {
        _input.SwitchCharacterSelection -= SwitchSelection;
        _input.OnDrag -= MoveCamera;
        _input.OnTap -= CommandPlayerCharacters;
    }

    private void CommandPlayerCharacters(Vector3 obj)
    {
        var ray = cameraRig.camera.ScreenPointToRay(obj);

        if (Physics.Raycast(ray, out var hit))
        {
            if (hit.collider.TryGetComponent<EnemyComponent>(out var enemy))
            {
                ScreenSystem.ScreensManager.GetScreen<GameHUDScreen>().SetTarget(enemy);

                foreach (var character in team.GetSelectedCharacters())
                {
                    var command = new AttackCommand(enemy);
                    character.ExecuteCommand(command);
                }          
            }
            else
            {
                ScreenSystem.ScreensManager.GetScreen<GameHUDScreen>().SetTarget(null);

                var characters = team.GetSelectedCharacters();
                for (int i = 0; i < characters.Length; i++)
                {
                    CharacterComponent character = characters[i];
                    var command = new MovingCommand(hit.point + Vector3.right * i);
                    character.ExecuteCommand(command);
                }
            }
        }
    }

    private void MoveCamera(Vector3 delta)
    {
        delta = new Vector3(-delta.x, 0, -delta.y) * 0.03f;
        cameraRig.transform.Translate(delta, Space.Self);
    }

}
