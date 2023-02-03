using FeatureSystem.Systems;
using QuestSystem;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singletone<GameManager>, IDisposable
{
    public void Initialize()
    {
        LevelEndSystem.LevelEnded += OnLevelEnded;
        LevelEndSystem.LevelFailed += OnLevelFailed;

        Application.quitting += OnApplicationQuitting;
    }

    public void Dispose()
    {
        LevelEndSystem.LevelEnded -= OnLevelEnded;
        LevelEndSystem.LevelFailed -= OnLevelFailed;

        Application.quitting -= OnApplicationQuitting;
    }

    private void OnApplicationQuitting()
    {
        if (SceneManager.GetActiveScene().name == "Home")
        {
            PlayerDataManager.Data.currentScene = "Home";
            PlayerDataManager.SaveData();
        }
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
        ObjectSpawnManager.Instance.Initialize();
        CurrencyManager.Instance.Initialize();
        QuestManager.Instance.Initialize();
    }

    public void GoToGameScene()
    {
        ScreenSystem.ScreensManager.HideScreen<HomeScreen>();
        SceneLoader.LoadGameScene(OnGameSceneLoaded);
    }

    private void OnLevelEnded()
    {
        UpdatePlayerData();

        ObjectSpawnManager.Instance.Dispose();
        GameSystems.DestroySystems();

        ScreenSystem.ScreensManager.HideScreen<GameHUDScreen>();

        ScreenSystem.ScreensManager.ShowScreen<LevelEndedScreen>()
            .SetButtonCallback(() => SceneLoader.LoadHomeScene(OnHomeSceneLoaded));
    }

    private void OnLevelFailed()
    {
        ObjectSpawnManager.Instance.Dispose();
        GameSystems.DestroySystems();

        ScreenSystem.ScreensManager.HideScreen<GameHUDScreen>();

        ScreenSystem.ScreensManager.ShowScreen<LevelFailedScreen>()
            .SetButtonsCallback(
            () => SceneLoader.LoadHomeScene(OnHomeSceneLoaded),
            () => SceneLoader.LoadGameScene(OnGameSceneLoaded));
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
