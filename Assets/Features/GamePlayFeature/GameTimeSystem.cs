using FeatureSystem.Systems;
using System.Collections;
using UnityEngine;

public class GameTimeSystem : ISystem, ISystemUpdate
{
    private InputController _input;

    public float DeltaTime => Time.deltaTime;
    public float IndependentDeltaTime => Time.unscaledDeltaTime;

    public float TimeSinceStart { get; private set; } = 0;

    public bool IsPaused { get; private set; }

    public void Destroy()
    {
        _input.SetPause -= SetPause;
    }

    public void Initialize()
    {
        _input = GameSystems.GetSystem<PlayerInputSystem>().PlayerInput;

        _input.SetPause += SetPause;
    }

    public void SetPause(bool paused)
    {
        IsPaused = paused;
        Time.timeScale = paused ? 0 : 1;
    }

    public void Update()
    {
        TimeSinceStart += DeltaTime;
    }

    public IEnumerator WaitForTime(float time)
    {
        float timer = 0;

        while(timer < time)
        {
            yield return null;
            timer += DeltaTime;
        }
    }

    public IEnumerator WaitForTimeIndependent(float time)
    {
        float timer = 0;

        while (timer < time)
        {
            yield return null;
            timer += IndependentDeltaTime;
        }
    }
}

public static class TimeUtils
{
    public static IEnumerator WaitForTime(float time)
    {
        return GameSystems.GetSystem<GameTimeSystem>().WaitForTime(time);
    }

    public static IEnumerator WaitForTimeIndependent(float time)
    {
        return GameSystems.GetSystem<GameTimeSystem>().WaitForTimeIndependent(time);
    }

    public static float GetDeltaTime()
    {
        return Time.deltaTime;
    }

    public static float GetIndependentDeltaTime()
    {
        return Time.unscaledDeltaTime;
    }

    public static float GetTimeSinceStart()
    {
        return GameSystems.GetSystem<GameTimeSystem>().TimeSinceStart;
    }
}

