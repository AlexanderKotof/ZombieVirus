using BuildingSystem;
using Features.CharactersFeature.Components;
using Features.CharactersFeature.Systems;
using Features.GamePlayFeature.Systems;
using FeatureSystem.Systems;
using PlayerDataSystem;
using QuestSystem;
using System;
using TeamSystem;

public class GameManager : Singletone<GameManager>, IDisposable
{
    public void Initialize()
    {
        LevelEndSystem.LevelEnded += OnLevelEnded;
        LevelEndSystem.LevelFailed += OnLevelFailed;
    }

    public void Dispose()
    {
        LevelEndSystem.LevelEnded -= OnLevelEnded;
        LevelEndSystem.LevelFailed -= OnLevelFailed;
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
        BuildingManager.Instance.Initialize();
        PlayerTeamManager.Instance.Initialize();
    }

    public void GoToGameScene()
    {
        PlayerDataManager.SaveData();

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
        PlayerDataManager.Data.currentScene = "Home";
        PlayerDataManager.SaveData();

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

        var charactersCount = teamSystem.Characters.Length;
        for (int i = 0; i < charactersCount; i++)
        {
            PlayerComponent character = teamSystem.Characters[i];
            var characterData = PlayerDataManager.Data.characterDatas[i];

            characterData.currentHealth = character.CurrentHealth;
            characterData.currentExp += experienceSystem.ExperienceCounter / charactersCount;
        }

        // Add founded items
    }
}
