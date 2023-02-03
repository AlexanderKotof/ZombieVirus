using QuestSystem;
using ScreenSystem.Components;

namespace Screens.HomeScreen.QuestWindow.Components
{
    public class QuestListItemComponent : ButtonComponent
    {
        public void SetQuest(Quest quest, bool completed)
        {
            text.SetText(quest.Name);

            image.enabled = completed;
        }
    }
}
