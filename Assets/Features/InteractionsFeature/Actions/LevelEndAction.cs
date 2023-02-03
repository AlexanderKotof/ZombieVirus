using Features.CharactersFeature.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndAction : InteractionAction
{
    public Condition[] requiredConditions;

    public override bool CanInteract(CharacterComponent character)
    {
        foreach(var condition in requiredConditions)
        {
            if (!condition.Satisfied())
                return false;
        }
        return true;
    }

    public override void Interact(CharacterComponent character, InteractionComponent interaction)
    {
        
    }
}

public abstract class Condition : ScriptableObject
{
    public abstract bool Satisfied();
}
