using UnityEngine;

public class CharacterPrototype : ScriptableObject 
{
    public CharacterComponent characterPrefab;

    public Sprite characterIcon;

    public string Name;

    public float health;

    public float damage;

    public float attackRange;

    public float attackSpeed;

    public float moveSpeed;
}

public static class CharacterSpawnUtils
{
    public static CharacterComponent SpawnCharacter(CharacterData data, Vector3 position, Quaternion rotation)
    {
        var character = CharacterComponent.Instantiate(data.prototype.characterPrefab, position, rotation);
        character.SetPrototype(data.prototype);
        return character;
    }
}
