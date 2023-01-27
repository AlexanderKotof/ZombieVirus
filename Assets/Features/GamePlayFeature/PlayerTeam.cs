using System;
using UnityEngine;

[Serializable]
public class PlayerTeam
{
    public PlayerComponent[] characters { get; private set; }

    public CharacterData[] charactersData;

    public void InstatiateCharacters(Transform spawnPoint)
    {
        charactersData = PlayerDataManager.Data.characterDatas;

        characters = new PlayerComponent[charactersData.Length];

        for (int i = 0; i < charactersData.Length; i++)
        {
            var position = spawnPoint.position + Vector3.right * i;
            characters[i] = CharacterSpawnUtils.SpawnCharacter(charactersData[i], position, spawnPoint.rotation) as PlayerComponent;
        }
    }


}
