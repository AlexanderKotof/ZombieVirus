using Features.CharactersFeature.Components;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Entities/Interactions/Collect Items")]
public class CollectItemsAction : InteractionAction
{
    public List<CollectItem> items;

    public override bool CanInteract(CharacterComponent character)
    {
        return true;
    }

    public override void Interact(CharacterComponent character, InteractionComponent component)
    {
        foreach(var item in items)
        {
            PlayerInventoryManager.Instance.CollectLevelItems(item.item, item.count);
        }
    }

    [Serializable]
    public struct CollectItem
    {
        public Item item;
        public int count;
    }
}
