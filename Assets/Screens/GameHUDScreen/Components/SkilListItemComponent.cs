using Features.SkillsFeature.Prototypes;
using ScreenSystem.Components;
using System;

public class SkilListItemComponent : WindowComponent
{
    public ButtonComponent useButton;

    public ImageComponent icon;

    public void SetInfo(SkillPrototype prototype, Action onClick)
    {
        useButton.SetCallback(onClick);

        icon.SetImage(prototype.icon);
    }
}
