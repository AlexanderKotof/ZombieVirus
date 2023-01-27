using UnityEngine;

public class SceneContext : MonoBehaviour
{
    public SpawnPointComponent[] enemiesSpawnPoints;

    public Transform playerSpawnPoint;

    public Transform exitPoint;

    private static SceneContext _instance { get; set; }

    public void Awake()
    {
        _instance = this;
    }

    internal static SceneContext GetSceneContext()
    {
        return _instance;
    }

    private void OnDestroy()
    {
        _instance = null;
    }

}
