using PlayerDataSystem;
using PlayerDataSystem.DataStructures;
using System.Collections.Generic;

namespace TeamSystem
{
    public class PlayerTeamManager : Singletone<PlayerTeamManager>
    {
        public CharacterData[] playerTeam;
        public List<int> charactersOnLevel;

        public void Initialize()
        {
            playerTeam = PlayerDataManager.Data.characterDatas;
            charactersOnLevel = new List<int>() { 0 };
        }

        public CharacterData[] GetCharactersOnLevel()
        {
            var characters = new CharacterData[charactersOnLevel.Count];

            for (int i = 0; i < characters.Length; i++)
            {
                characters[i] = playerTeam[charactersOnLevel[i]];
            }

            return characters;
        }

        public bool CanSelect(int index)
        {
            return playerTeam[index].currentHealth > 0;
        }
    }
}