using Features.CharactersFeature.Components;
using UnityEngine;

public abstract class InteractionAction : ScriptableObject
{
    public abstract bool CanInteract(CharacterComponent character);
    public abstract void Interact(CharacterComponent character, InteractionComponent interaction);
}
