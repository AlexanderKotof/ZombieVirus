using System;
using UnityEngine;

[Serializable]
public class PlayerTeam
{
    public PlayerComponent[] characters { get; private set; }

    public CharacterPrototype[] prototypes;

    public int selectedCharacters = -1;

    public void InstatiateCharacters(Transform spawnPoint)
    {
        characters = new PlayerComponent[prototypes.Length];

        for (int i = 0; i < prototypes.Length; i++)
        {
            var position = spawnPoint.position + Vector3.right * i;
            characters[i] = prototypes[i].SpawnCharacter(position, spawnPoint.rotation) as PlayerComponent;
        }
    }

    public CharacterComponent[] GetSelectedCharacters()
    {
        if (selectedCharacters == -1)
        {
            return characters;
        }
        else
        {
            return new[] { characters[selectedCharacters] };
        }
    }
}
