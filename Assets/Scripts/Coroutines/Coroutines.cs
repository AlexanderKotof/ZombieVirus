using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coroutines : MonoBehaviour
{
    private static Coroutines _instance;

    private static void CreateCoroutines()
    {
        var coroutines = new GameObject("Coroutines").AddComponent<Coroutines>();
        _instance = coroutines;
        DontDestroyOnLoad(coroutines);
    }

    public static Coroutine Run(IEnumerator coroutine)
    {
        if (!_instance)
            CreateCoroutines();

        return _instance.StartCoroutine(coroutine);
    }

    public static void Stop(IEnumerator coroutine)
    {
        if (!_instance)
            CreateCoroutines();

        _instance.StopCoroutine(coroutine);
    }

    public static void Stop(Coroutine coroutine)
    {
        if (!_instance)
            CreateCoroutines();

        _instance.StopCoroutine(coroutine);
    }

    public static void StopAll()
    {
        if (!_instance)
            CreateCoroutines();

        _instance.StopAllCoroutines();
    }
}
