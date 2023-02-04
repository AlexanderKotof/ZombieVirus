using Screens.HomeScreen.BuildingWindow.Components;
using ScreenSystem.Components;
using System.Collections;

namespace Screens.HomeScreen.BuildingWindow
{
    public class BuildingWindowComponent : WindowComponent
    {
        public ListComponent buildingsList;


        protected override void OnShow()
        {
            base.OnShow();

            var manager = BuildingSystem.BuildingManager.Instance;

            buildingsList.SetItems<BuildingsListItemComponent>(manager.readyForBuild.Count, (item, par) =>
            {
                item.SetInfo(manager.readyForBuild[par.index], () => manager.Build(manager.readyForBuild[par.index]));
            });
        }
    }
}
