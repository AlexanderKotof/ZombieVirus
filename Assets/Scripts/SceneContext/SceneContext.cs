using UnityEngine;

public class SceneContext : MonoBehaviour
{
    public SpawnPointComponent[] enemiesSpawnPoints;

    public Transform playerSpawnPoint;

    public Transform exitPoint;

    public Bounds cameraBounds;

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

    private void OnDrawGizmosSelected()
    {
        Vector3[] points = new Vector3[]
        {
            cameraBounds.max,
            cameraBounds.max - Vector3.forward * cameraBounds.extents.z * 2,
            cameraBounds.min,
            cameraBounds.min + Vector3.forward * cameraBounds.extents.z * 2,
        };

        for (int i = 0; i < points.Length; i++)
        {
            if (i < points.Length - 1)
                Debug.DrawLine(points[i], points[i + 1], Color.green);
            else
                Debug.DrawLine(points[i], points[0], Color.green);
        }
    }
}
