using Features.CharactersFeature.Components;
using Features.SkillsFeature.Prototypes;
using System;
using UnityEngine;

namespace Features.CharactersFeature.Prototypes
{
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

        public SkillPrototype[] skills;

        public float health;

        public float damage;

        public float defence;

        public float attackRange;

        public float attackSpeed;

        public float moveSpeed;

        public int ExperienceReward;

        public Fraction fraction;

        public Weapon defaultWeapon;

        public Armor defaultArmor;

        public enum Fraction
        {
            Human,
            Undead,
        }

        int IHasId.Id => Id;
    }
}