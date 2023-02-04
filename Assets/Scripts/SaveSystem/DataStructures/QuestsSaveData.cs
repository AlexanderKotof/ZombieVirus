using QuestSystem;
using System;

namespace SaveSystem.DataStructures
{

    [Serializable]
    public class QuestsSaveData
    {
        public QuestData[] curentQuestsData;
        public QuestData[] complitedQuestsData;

        [Serializable]
        public class QuestData
        {
            public int questId;
            public int[] completedStages;
        }

    }

}

