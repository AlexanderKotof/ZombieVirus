using System;

namespace SaveSystem.DataStructures
{
    [Serializable]
    public class BuildingsSaveData
    {
        public int[] buildedIds;
        public int[] readyToBuildIds;

        public int buldingProgressId;
        public DateTime startedAt;
    }

}

