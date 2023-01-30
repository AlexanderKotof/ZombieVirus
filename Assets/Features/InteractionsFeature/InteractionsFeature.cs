using FeatureSystem.Features;
using FeatureSystem.Systems;
using UnityEngine;

public class InteractionsFeature : Feature
{
    public override void Initialize()
    {
        GameSystems.RegisterSystem(new InteractionSystem());
    }
}

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

        GameObject.Destroy(interaction.gameObject);
    }
}
