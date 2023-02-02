using System.Collections;
using System.Collections.Generic;

public class QuestManager : Singletone<QuestManager>
{
    public List<Quest> currentQuests;

    public void Initialize()
    {
        var questIds = PlayerDataManager.Data.questsData.currentQuestIds;

        currentQuests = new List<Quest>(questIds.Length);

        foreach (var id in questIds)
        {
            currentQuests.Add(QuestUtils.GetQuest(id));
        }
    }

    public bool CompleteQuest(Quest quest)
    {
        if (!currentQuests.Contains(quest))
            return false;

        currentQuests.Remove(quest);

        foreach(var item in quest.reward)
        {

        }

        if (quest.nextQuests != null && quest.nextQuests.Length > 0)
            currentQuests.AddRange(quest.nextQuests);

        return true;
    }
}

