using System;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public PlayerData defaultData;
    public static PlayerData Data { get; private set; }

    private static PlayerDataManager _instance;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        _instance = this;
    }

    public static PlayerData LoadOrCreateNewData()
    {
        Data = SaveGameSystem.SaveLoadData.Load();

        if (Data != null)
            return Data;

        Data = _instance.defaultData;

        return Data;
    }

    public static void SaveData()
    {
        SaveGameSystem.SaveLoadData.SaveFile(Data);
    }
}

[Serializable]
public class PlayerData
{
    public CharacterData[] characterDatas;
    public InventoryData inventoryData;
    public QuestsData questsData;
}

[Serializable]
public class QuestsData
{
    public int[] currentQuestIds;
}

[Serializable]
public class CharacterData
{
    public CharacterPrototype prototype;

    public int currentExp;

    public Weapon weapon;

    public Armor armor;
}

[Serializable]
public class InventoryData
{
    public int[] itemsIds;
}
