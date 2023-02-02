using FeatureSystem.Systems;
using System;
using UnityEngine;

public class AppStartup : MonoBehaviour
{
    private void Start()
    {
        var screen = ScreenSystem.ScreensManager.ShowScreen<MainMenuScreen>();
        screen.startButton.AddCallback(StartGame);
        screen.exitButton.AddCallback(Exit);
        screen.clearDataButton.AddCallback(ClearData);
    }

    private void ClearData()
    {
        PlayerDataManager.ClearData();
    }

    private void StartGame()
    {
        GameManager.Instance.Initialize();
        GameManager.Instance.StartGame();
    }

    private void Exit()
    {
        Application.Quit();
    }
}
