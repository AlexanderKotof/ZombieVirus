using UnityEngine;

public static class QuestUtils
{
    private static QuestStorage _questStorage;

    private static void LoadStorage()
    {
        _questStorage = Resources.Load<QuestStorage>(nameof(QuestStorage));
    }

    public static Quest GetQuest(int id)
    {
        if (!_questStorage)
            LoadStorage();

        return _questStorage.GetItem(id);
    }
}

