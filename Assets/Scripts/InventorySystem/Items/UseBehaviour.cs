using Features.CharactersFeature.Components;
using UnityEngine;

public abstract class UseBehaviour : ScriptableObject
{
    public abstract void Use(CharacterComponent byCharacter);
}
