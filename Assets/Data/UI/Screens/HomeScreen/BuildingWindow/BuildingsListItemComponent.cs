using BuildingSystem;
using BuildingSystem.Prototypes;
using ScreenSystem.Components;
using System;
using UI.SharedComponents;
using UnityEngine.UI;

namespace UI.Screens.HomeScreen.BuildingWindow.Components
{
    public class BuildingsListItemComponent : WindowComponent
    {
        public TextComponent buildingName;
        public ImageComponent buildingIcon;

        public ListComponent requiredResources;

        public TextComponent buildingTimeText;

        public ButtonComponent buildButton;

        public Slider buildingProgress;
        public TextComponent timeLeftText;
        private BuildingPrototype _building;

        public void SetInfo(BuildingPrototype building, Action buildButtonCallback)
        {
            _building = building;

            buildButton.SetCallback(buildButtonCallback);

            buildingName.SetText(building.Name);
            buildingIcon.SetImage(building.Icon);

            UpdateInfo();
        }

        public void UpdateInfo()
        {
            var manager = BuildingManager.Instance;
            if (manager.IsBuilds(_building, out var timeLeft))
            {
                buildingProgress.gameObject.SetActive(true);
                timeLeftText.Show();
                buildButton.Hide();
                requiredResources.Hide();

                buildingProgress.maxValue = _building.buildingTimeSec;
                buildingProgress.value = _building.buildingTimeSec - timeLeft;

                timeLeftText.SetText($"{timeLeft}sec");
            }
            else
            {
                buildingProgress.gameObject.SetActive(false);
                timeLeftText.Hide();
                buildButton.Show();
                requiredResources.Show();

                buildingTimeText.SetText($"{_building.buildingTimeSec}sec");

                requiredResources.SetItems<InventoryListItem>(_building.requiredResources.Length, (item, par) =>
                {
                    item.SetInfo(_building.requiredResources[par.index]);
                });

                buildButton.SetInteractable(manager.CanBuild(_building));
            }
        }
    }
}
