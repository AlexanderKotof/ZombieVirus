using System;

namespace SaveSystem.DataStructures
{
    [Serializable]
    public class BuildingsData
    {
        public int[] buildedIds;
        public int[] readyToBuildIds;

        public int buldingProgressId;
        public DateTime startedAt;
    }

}

