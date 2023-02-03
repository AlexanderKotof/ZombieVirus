using Features.CharactersFeature.Components;
using Features.InteractionFeature.Actions;
using System;
using UnityEngine;

namespace Features.InteractionFeature.Components
{
    public class InteractionComponent : MonoBehaviour
    {
        public InteractionAction[] actions;
        public static event Action<CharacterComponent, InteractionComponent> Interact;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerComponent>(out var player))
            {
                Interact?.Invoke(player, this);
            }
        }
    }
}