using Features.CharactersFeature.Prototypes;
using SaveSystem;
using SaveSystem.DataStructures;
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
        Data = SaveLoadSystem.Load();

        if (Data != null)
            return Data;

        Data = _instance.defaultData;

        return Data;
    }

    public static void ClearData()
    {
        SaveLoadSystem.ClearPlayerData();
    }

    public static void SaveData()
    {
        SaveLoadSystem.SaveFile(Data);
    }
}

[Serializable]
public class PlayerData
{
    public CharacterData[] characterDatas;

    public InventorySaveData inventoryData;

    public QuestsSaveData questsData;

    public string currentScene;
}


[Serializable]
public class CharacterData
{
    public CharacterPrototype prototype;

    public float currentHealth;

    public int currentExp;

    public Weapon weapon;

    public Armor armor;
}
