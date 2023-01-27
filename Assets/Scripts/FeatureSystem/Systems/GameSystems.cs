using System;
using System.Collections.Generic;
using UnityEngine;

namespace FeatureSystem.Systems
{
    public class GameSystems : MonoBehaviour
    {
        private static GameSystems _instance;
        public static Dictionary<Type, ISystem> Systems => _instance._systems;

        private Dictionary<Type, ISystem> _systems = new Dictionary<Type, ISystem>();

        private readonly List<ISystemUpdate> _updateSystems = new List<ISystemUpdate>();

        public void Awake()
        {
            DontDestroyOnLoad(this);
            _instance = this;
        }

        private void Update()
        {
            UpdateSystems();
        }

        public static void RegisterSystem(ISystem system)
        {
            _instance._systems.Add(system.GetType(), system);

            if (system is ISystemUpdate updateSystem)
            {
                _instance._updateSystems.Add(updateSystem);
            }
        }

        public static T GetSystem<T>() where T : ISystem
        {
            if (_instance._systems.TryGetValue(typeof(T), out var system))
                return (T)system;

            Debug.LogError($"No system of type {typeof(T).Name} was founded!");
            return default;
        }

        public void UpdateSystems()
        {
            foreach (var system in _updateSystems)
            {
                system.Update();
            }
        }

        public static void DestroySystems()
        {
            foreach (var system in _instance._systems.Values)
            {
                system.Destroy();
            }

            _instance._systems.Clear();
            _instance._updateSystems.Clear();
        }
    }
}