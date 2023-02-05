using ScreenSystem.Components;
using System.Collections;
using UI.Screens.HomeScreen.BuildingWindow.Components;

namespace UI.Screens.HomeScreen.BuildingWindow
{
    public class BuildingWindowComponent : WindowComponent
    {
        public ListComponent buildingsList;

        protected override void OnShow()
        {
            base.OnShow();

            var manager = BuildingSystem.BuildingManager.Instance;
            manager.BuildingsUpdated += OnBuildingsUpdated;

            OnBuildingsUpdated();
        }

        protected override void OnHide()
        {
            var manager = BuildingSystem.BuildingManager.Instance;
            manager.BuildingsUpdated -= OnBuildingsUpdated;

            base.OnHide();
        }

        private void OnBuildingsUpdated()
        {
            var manager = BuildingSystem.BuildingManager.Instance;
            buildingsList.SetItems<BuildingsListItemComponent>(manager.readyForBuild.Count, (item, par) =>
            {
                item.SetInfo(manager.readyForBuild[par.index], () => manager.Build(manager.readyForBuild[par.index]));
            });
        }

        private void Update()
        {
            foreach (BuildingsListItemComponent item in buildingsList.items)
            {
                item.UpdateInfo();
            }
        }
    }
}
