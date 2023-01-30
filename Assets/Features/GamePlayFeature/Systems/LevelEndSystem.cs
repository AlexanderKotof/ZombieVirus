using FeatureSystem.Systems;
using System;
using UnityEngine;

public class LevelEndSystem : ISystem, ISystemUpdate
{
    private Transform _levelEndPoint;
    private PlayerTeamSystem _playerTeamSystem;

    private float _checkDistance = 2f;

    public static event Action LevelEnded;

    public void Initialize()
    {
        _levelEndPoint = SceneContext.GetSceneContext().exitPoint;
        _playerTeamSystem = GameSystems.GetSystem<PlayerTeamSystem>();

    }

    public void Destroy()
    {
        
    }

    public void Update()
    {
        int charactersCount = 0;
        foreach(var character in _playerTeamSystem.team.characters)
        {
            if (character.IsDied || (character.Position - _levelEndPoint.position).sqrMagnitude < _checkDistance * _checkDistance)
            {
                charactersCount++;
            }
        }

        if (charactersCount == _playerTeamSystem.team.characters.Length)
        {
            LevelEnded?.Invoke();
        }
    }
}
