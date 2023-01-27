using System;
using UnityEngine;

public class InputController
{
    public Action<Vector3> OnTap;

    public Action<Vector3> OnDrag;

    public Action<int> SwitchCharacterSelection;
}
