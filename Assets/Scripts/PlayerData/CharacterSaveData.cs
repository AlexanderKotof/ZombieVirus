using System;

namespace SaveGameSystem
{

    [Serializable]
    public class CharacterSaveData
    {
        public int prototypeId;

        public float currentHealth;

        public int currentExp;

        public int weaponId;

        public int armorId;
    }

}

