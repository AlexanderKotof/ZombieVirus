using ScreenSystem.Components;
using UnityEngine.UI;

namespace Screens.HomeScreen.QuestWindow.Components
{
    public class QuestStageListItemComponent : WindowComponent
    {
        public Image chackmark;
        public TextComponent text;
        public void SetInfo(string description, bool completed)
        {
            text.SetText(description);
            chackmark.enabled = completed;
        }
    }
}
