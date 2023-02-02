using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnManager : Singletone<ObjectSpawnManager>, IDisposable
{
    private readonly Dictionary<Component, ObjectPool<Component>> _componentToObjectPools = new Dictionary<Component, ObjectPool<Component>>();

    private Transform _parent;

    public void Initialize()
    {
        _parent = new GameObject("ObjectsPool").transform;
    }

    public void Dispose()
    {
        foreach (var pool in _componentToObjectPools.Values)
        {
            pool.Dispose();
        }
        _componentToObjectPools.Clear();
    }

    private ObjectPool<Component> RegisterPrefab(Component prefab)
    {
        if (_componentToObjectPools.TryGetValue(prefab, out var pool))
        {
            return pool;
        }

        Debug.Log($"Registered new prefab {prefab.gameObject.name} of type {prefab.GetType().Name}");

        pool = new ObjectPool<Component>(prefab, _parent);
        _componentToObjectPools.Add(prefab, pool);

        return pool;
    }

    public static Component Spawn(Component prefab)
    {
        var pool = Instance.RegisterPrefab(prefab);
        return pool.Pool();
    }

    public static T Spawn<T>(T component) where T : Component
    {
        return Spawn((Component)component) as T;
    }

    public static void Despawn(Component instance)
    {
        instance.gameObject.SetActive(false);
    }
}
