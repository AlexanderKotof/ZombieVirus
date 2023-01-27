using System;
using System.Collections;
using UnityEngine;

public abstract class Command
{
    public Action<Command> Executed;

    public abstract void BeginExecute(CharacterComponent character);

    public abstract void StopExecute();
}

public interface IUpdateCommand
{
    void Update();
}
