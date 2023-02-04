using BuildingSystem;
using BuildingSystem.Prototypes;
using ScreenSystem.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Screens.HomeScreen.BuildingWindow.Components
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

        public void SetInfo(BuildingPrototype building, Action buildButtonCallback)
        {
            var manager = BuildingManager.Instance;

            buildingName.SetText(building.Name);
            buildingIcon.SetImage(building.Icon);

            if (manager.IsBuilds(building, out var timeLeft))
            {
                buildingProgress.gameObject.SetActive(true);
                timeLeftText.Show();
                buildButton.Hide();
                requiredResources.Hide();

                buildingProgress.maxValue = building.buildingTimeSec;
                buildingProgress.value = building.buildingTimeSec - timeLeft;

                timeLeftText.SetText($"{timeLeft}sec");
            }
            else
            {
                buildingProgress.gameObject.SetActive(false);
                timeLeftText.Hide();
                buildButton.Show();
                requiredResources.Show();

                buildingTimeText.SetText($"{building.buildingTimeSec}sec");

                //requiredResources.SetItems<>()

                buildButton.SetInteractable(manager.CanBuild(building));

                buildButton.SetCallback(buildButtonCallback);
            }
        }
    }
}
