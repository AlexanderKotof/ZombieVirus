using Features.CharactersFeature.Components;

public class HealBehaviour : UseBehaviour
{
    public float healValue;

    public override void Use(CharacterComponent byCharacter)
    {
        byCharacter.Heal(healValue);
    }
}
