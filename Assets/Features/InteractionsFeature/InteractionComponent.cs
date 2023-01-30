using System;
using UnityEngine;

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
