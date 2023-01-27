using System;
using UI.Screens;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private const string _gameSceneName = "Game";
    private const string _homeSceneName = "Home";

    private static Action _callback;

    public static void LoadGameScene(Action callback)
    {
        LoadScene(_gameSceneName, callback);
    }

    public static void LoadHomeScene(Action callback)
    {
        LoadScene(_homeSceneName, callback);
    }

    public static void LoadScene(string sceneName, Action callback)
    {
        _callback = callback;
        ScreenSystem.ScreensManager.ShowScreen<LoadingScreen>();

        var operation = SceneManager.LoadSceneAsync(sceneName);
        operation.completed += OnLoaded;
    }

    private static void OnLoaded(AsyncOperation operation)
    {
        operation.completed -= OnLoaded;

        ScreenSystem.ScreensManager.HideScreen<LoadingScreen>();

        _callback?.Invoke();
    }
}
