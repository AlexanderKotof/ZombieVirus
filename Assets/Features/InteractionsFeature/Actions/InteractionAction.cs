using Features.CharactersFeature.Components;
using Features.InteractionFeature.Components;
using UnityEngine;

namespace Features.InteractionFeature.Actions
{
    public abstract class InteractionAction : ScriptableObject
    {
        public abstract bool CanInteract(CharacterComponent character);
        public abstract void Interact(CharacterComponent character, InteractionComponent interaction);
    }
}