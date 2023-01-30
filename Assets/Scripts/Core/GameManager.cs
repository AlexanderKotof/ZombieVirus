using FeatureSystem.Systems;
using System;
using UnityEngine;

public class GameManager : Singletone<GameManager>, IDisposable
{
    public void Initialize()
    {
        LevelEndSystem.LevelEnded += OnLevelEnded;
        Application.quitting += OnApplicationQuitting;
    }

    public void Dispose()
    {
        LevelEndSystem.LevelEnded -= OnLevelEnded;
        Application.quitting -= OnApplicationQuitting;
    }

    private void OnApplicationQuitting()
    {
        PlayerDataManager.SaveData();
    }

    public void StartGame()
    {
        // Look for saved data
        // If not founded -> Start new game
        PlayerDataManager.LoadOrCreateNewData();

        ScreenSystem.ScreensManager.HideScreen<MainMenuScreen>();

        if (PlayerDataManager.Data.currentScene == "Home")
        {
            SceneLoader.LoadHomeScene(OnHomeSceneLoaded);
        }
        else
            SceneLoader.LoadScene(PlayerDataManager.Data.currentScene, OnGameSceneLoaded);

        PlayerInventoryManager.Instance.InitializeInventory();
    }

    private void OnLevelEnded()
    {
        UpdatePlayerData();

        GameSystems.DestroySystems();
        ScreenSystem.ScreensManager.HideScreen<GameHUDScreen>();
        SceneLoader.LoadHomeScene(OnHomeSceneLoaded);
    }

    private void OnHomeSceneLoaded()
    {
        ScreenSystem.ScreensManager.ShowScreen<HomeScreen>();
    }

    private void OnGameSceneLoaded()
    {
        FeatureSystem.Features.Features.InitializeFeaturesAndSystems();

        ScreenSystem.ScreensManager.ShowScreen<GameHUDScreen>();
    }

    private void UpdatePlayerData()
    {
        var teamSystem = GameSystems.GetSystem<PlayerTeamSystem>();
        var experienceSystem = GameSystems.GetSystem<ExperienceSystem>();

        var charactersCount = teamSystem.team.characters.Length;
        for (int i = 0; i < charactersCount; i++)
        {
            PlayerComponent character = teamSystem.team.characters[i];
            var characterData = PlayerDataManager.Data.characterDatas[i];

            characterData.currentHealth = character.CurrentHealth;
            characterData.currentExp += experienceSystem.ExperienceCounter / charactersCount;
        }

        // Add founded items
    }
}
