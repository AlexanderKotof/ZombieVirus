using FeatureSystem.Features;
using FeatureSystem.Systems;
using System.Collections.Generic;

public class CharactersFeature : Feature
{
    public override void Initialize()
    {
        GameSystems.RegisterSystem(new SpawnCharacterSystem());
    }
}

public class SpawnCharacterSystem : ISystem
{
    public List<CharacterComponent> spawnedCharacters;

    public void Initialize()
    {

    }

    public void Destroy()
    {
        
    }
}
