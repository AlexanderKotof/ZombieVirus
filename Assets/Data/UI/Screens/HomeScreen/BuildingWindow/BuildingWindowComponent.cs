using BuildingSystem.Prototypes;
using ScreenSystem.Components;
using System.Collections;
using System.Collections.Generic;
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

            var buildings = new List<BuildingPrototype>();
            buildings.AddRange(manager.builded);
            buildings.AddRange(manager.readyForBuild);

            buildingsList.SetItems<BuildingsListItemComponent>(buildings.Count, (item, par) =>
            {
                item.SetInfo(buildings[par.index], () => manager.Build(buildings[par.index]));
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
