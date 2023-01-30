using UnityEngine;

public class Item : ScriptableObject, IHasId
{
    public string Name;

    public int Id;

    public Sprite icon;

    int IHasId.Id => Id;
}
