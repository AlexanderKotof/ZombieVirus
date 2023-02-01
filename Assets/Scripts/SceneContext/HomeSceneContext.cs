using UnityEngine;

public class HomeSceneContext : MonoBehaviour, ISceneContext
{
    private static HomeSceneContext _instance;

    public Transform characterPreviewTransform;

    private void Awake()
    {
        _instance = this;
    }

    internal static HomeSceneContext GetSceneContext()
    {
        return _instance;
    }
}
