using FeatureSystem.Features;
using FeatureSystem.Systems;
using System.Collections.Generic;
using UnityEngine;

public class CharactersFeature : Feature
{
    public override void Initialize()
    {
        GameSystems.RegisterSystem(new SpawnCharacterSystem());
        GameSystems.RegisterSystem(new ExperienceSystem());
    }
}

public class SpawnCharacterSystem : ISystem
{
    public List<CharacterComponent> spawnedCharacters = new List<CharacterComponent>();
    public List<CharacterComponent> spawnedEnemies = new List<CharacterComponent>();

    public void Initialize()
    {

    }

    public void Destroy()
    {
        
    }

    public CharacterComponent SpawnPlayerCharacter(CharacterData data, Vector3 position, Quaternion rotation)
    {
        var character = CharacterSpawnUtils.SpawnCharacter(data, position, rotation);
        character.CurrentHealth = data.currentHealth;
        spawnedCharacters.Add(character);
        return character;
    }

    public CharacterComponent SpawnEnemy(CharacterPrototype prototype, Vector3 position, Quaternion rotation)
    {
        var enemy = CharacterSpawnUtils.SpawnCharacter(prototype, position, rotation);
        spawnedEnemies.Add(enemy);
        return enemy;
    }
}
public class ExperienceSystem : ISystem
{
    private SpawnCharacterSystem _characterSpawnSystem;

    public int ExperienceCounter { get; private set; } = 0;

    public void Initialize()
    {
        _characterSpawnSystem = GameSystems.GetSystem<SpawnCharacterSystem>();

        foreach (var enemy in _characterSpawnSystem.spawnedEnemies)
        {
            enemy.Died += OnEnemyDied;
        }
    }

    private void OnEnemyDied(CharacterComponent enemy)
    {
        enemy.Died -= OnEnemyDied;
        ExperienceCounter += enemy.Data.prototype.ExperienceReward;
    }

    public void Destroy()
    {

    }
}
