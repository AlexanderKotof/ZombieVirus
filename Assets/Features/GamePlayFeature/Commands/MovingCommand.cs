using Features.CharactersFeature.Components;
using System;
using System.Collections;
using UnityEngine;

public class MovingCommand : Command
{
    private Vector3 destination;
    private CharacterComponent _character;

    private Coroutine _coroutine;

    private float _checkDistance = 0.1f;

    public MovingCommand(Vector3 destination) 
    {
        this.destination = destination;
    }

    public override void BeginExecute(CharacterComponent character)
    {
        _character = character;
        _character.MoveTo(destination);

        _coroutine = _character.StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        while ((_character.Position - destination).sqrMagnitude > _checkDistance)
        {
            yield return null;
        }

        Executed?.Invoke(this);
    }

    public override void StopExecute()
    {
        _character.StopCoroutine(_coroutine);
        _character.Stop();
    }
}
