using Features.CharactersFeature.Components;
using FeatureSystem.Systems;

public class PlayerTeamSystem : ISystem
{
    public PlayerTeam team;

    public int selectedCharacters = -1;

    public void Initialize()
    {
        var sceneContext = SceneContext.GetSceneContext();
        team = new PlayerTeam();
        team.InstatiateCharacters(sceneContext.playerSpawnPoint);
    }

    public CharacterComponent[] GetSelectedCharacters()
    {
        if (selectedCharacters == -1)
        {
            return team.characters;
        }
        else
        {
            return new[] { team.characters[selectedCharacters] };
        }
    }


    public void Destroy()
    {
        // destroy characters
    }


}
