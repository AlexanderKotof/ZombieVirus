using System;

namespace SaveGameSystem
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

