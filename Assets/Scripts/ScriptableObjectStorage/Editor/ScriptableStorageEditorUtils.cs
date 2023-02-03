using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using QuestSystem;

public static class ScriptableStorageEditorUtils
{
    [MenuItem("Tools/Validate Scriptable Storages")]
    public static void ValidateScriptableStorages()
    {
        ValidateStorage<ItemsStorage, Item>();
        ValidateStorage<CharactersStorage, CharacterPrototype>();
        ValidateStorage<QuestStorage, Quest>();
    }

    private static void ValidateStorage<Ts, Ti>() where Ts : ScriptableObjectStorage<Ti> where Ti : ScriptableObject, IHasId
    {
        var assetsGuids = AssetDatabase.FindAssets($"t:{typeof(Ts)}");

        var asset = AssetDatabase.LoadAssetAtPath<Ts>(AssetDatabase.GUIDToAssetPath(assetsGuids[0]));
        var itemsGuids = AssetDatabase.FindAssets($"t:{typeof(Ti)}");

        asset.items = new Ti[itemsGuids.Length];

        var idList = new List<int>();

        for (int i = 0; i < itemsGuids.Length; i++)
        {
            string guid = itemsGuids[i];

            asset.items[i] = AssetDatabase.LoadAssetAtPath<Ti>(AssetDatabase.GUIDToAssetPath(guid));

            if (idList.Contains(asset.items[i].Id))
            {
                Debug.LogWarning($"Item with id {asset.items[i]} already contains in array", asset.items[i]);
            }
            else
                idList.Add(asset.items[i].Id);
        }

        AssetDatabase.SaveAssetIfDirty(asset);

        Debug.Log("Succesfully validated " + typeof(Ts));
    }
}
