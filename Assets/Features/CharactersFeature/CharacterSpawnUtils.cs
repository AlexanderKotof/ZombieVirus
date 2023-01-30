using UnityEngine;

public static class CharacterSpawnUtils
{
    public static CharacterComponent SpawnCharacter(CharacterData data, Vector3 position, Quaternion rotation)
    {
        var character = CharacterComponent.Instantiate(data.prototype.characterPrefab, position, rotation);
        character.SetData(data);
        return character;
    }

    public static CharacterComponent SpawnCharacter(CharacterPrototype prototype, Vector3 position, Quaternion rotation)
    {
        var character = CharacterComponent.Instantiate(prototype.characterPrefab, position, rotation);
        var data = new CharacterData()
        {
            currentHealth = prototype.health,
            currentExp = 0,
            prototype = prototype,
        };
        character.SetData(data);
        return character;
    }
}

public static class CharactersUtils
{
    private static CharactersStorage _storage;

    private static void LoadStorage()
    {
        _storage = Resources.Load<CharactersStorage>(nameof(CharactersStorage));
    }

    public static CharacterPrototype GetPrototype(int id)
    {
        if (_storage == null)
            LoadStorage();

        return _storage.GetItem(id);
    }
}
