using Features.CharactersFeature.Components;
using Features.InteractionFeature.Components;
using FeatureSystem.Systems;
using UnityEngine;

namespace Features.InteractionFeature.Systems
{
    public class InteractionSystem : ISystem
    {
        public void Initialize()
        {
            InteractionComponent.Interact += OnInteract;
        }
        public void Destroy()
        {
            InteractionComponent.Interact -= OnInteract;
        }

        private void OnInteract(CharacterComponent character, InteractionComponent interaction)
        {
            foreach (var action in interaction.actions)
            {
                if (action.CanInteract(character))
                {
                    action.Interact(character, interaction);
                }
            }

            Object.Destroy(interaction.gameObject);
        }
    }
}