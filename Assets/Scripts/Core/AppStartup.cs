using System;
using UnityEngine;

public class AppStartup : MonoBehaviour
{
    private void Start()
    {
        var screen = ScreenSystem.ScreensManager.ShowScreen<MainMenuScreen>();
        screen.startButton.AddCallback(StartGame);
        screen.exitButton.AddCallback(Exit);
    }

    private void StartGame()
    {
        // Look for saved data
        // If not founded -> Start new game

        PlayerDataManager.LoadOrCreateNewData();

        ScreenSystem.ScreensManager.HideScreen<MainMenuScreen>();
  
        SceneLoader.LoadGameScene(OnGameSceneLoaded);

        PlayerInventoryManager.Instance.InitializeInventory();
    }

    private void OnGameSceneLoaded()
    {
        FeatureSystem.Features.Features.InitializeFeaturesAndSystems();

        ScreenSystem.ScreensManager.ShowScreen<GameHUDScreen>();
    }

    private void Exit()
    {
        Application.Quit();
    }

}
