using System;
using UnityEngine;

public class InputController : IDisposable
{
    public event Action<Vector3> OnTap;

    public event Action<Vector3> OnDrag;

    public event Action<int> SwitchCharacterSelection;


    public void TapInput(Vector3 val) => OnTap.Invoke(val);

    public void DragInput(Vector3 val) => OnDrag.Invoke(val);

    public void SwaitchCharactersInput(int val) => SwitchCharacterSelection.Invoke(val);


    public void Dispose()
    {
        OnTap = null;
        OnDrag = null;
        SwitchCharacterSelection = null;
    }
}
