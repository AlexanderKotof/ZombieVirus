using Features.CharactersFeature.Components;

public class UsableItem : Item
{
    public UseBehaviour useBehaviour;

    public void Use(CharacterComponent byCharacter)
    {
        useBehaviour.Use(byCharacter);
    }
}
