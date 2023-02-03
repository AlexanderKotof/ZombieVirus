using UnityEngine;

namespace Features.InteractionFeature.Conditions
{
    public abstract class Condition : ScriptableObject
    {
        public abstract bool Satisfied();
    }
}
