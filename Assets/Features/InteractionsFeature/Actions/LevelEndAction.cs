using Features.CharactersFeature.Components;
using Features.InteractionFeature.Components;
using Features.InteractionFeature.Conditions;
using System.Collections;
using System.Collections.Generic;

namespace Features.InteractionFeature.Actions
{
    public class LevelEndAction : InteractionAction
    {
        public Condition[] requiredConditions;

        public override bool CanInteract(CharacterComponent character)
        {
            foreach (var condition in requiredConditions)
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
}
