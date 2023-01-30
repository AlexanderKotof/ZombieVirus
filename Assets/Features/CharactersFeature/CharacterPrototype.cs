using System;
using UnityEngine;

public class CharacterPrototype : ScriptableObject, IHasId
{
    public int Id;

    [Serializable]
    public class CharacterMetaData
    {
        public Sprite characterIcon;

        public string Name;

        public string Info;
    }

    public CharacterMetaData metaData;

    public CharacterComponent characterPrefab;

    public float health;

    public float damage;

    public float attackRange;

    public float attackSpeed;

    public float moveSpeed;

    public int ExperienceReward;

    int IHasId.Id => Id;
}

