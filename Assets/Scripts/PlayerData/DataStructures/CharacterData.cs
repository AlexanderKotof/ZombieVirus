using Features.CharactersFeature.Prototypes;
using System;

namespace PlayerDataSystem.DataStructures
{
    [Serializable]
    public class CharacterData
    {
        public CharacterPrototype prototype;

        public float currentHealth;

        public int currentExp;

        public Weapon weapon;

        public Armor armor;
    }
}