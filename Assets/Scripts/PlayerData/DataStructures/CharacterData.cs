using Features.CharactersFeature.Prototypes;
using System;

namespace PlayerDataSystem.DataStructures
{
    [Serializable]
    public class CharacterData
    {
        public float maxHealth;

        public float currentHealth;

        public int prototypeId;

        public int currentExp;

        public int weaponId;

        public int armorId;

        public DateTime returnedAtHome;
    }
}