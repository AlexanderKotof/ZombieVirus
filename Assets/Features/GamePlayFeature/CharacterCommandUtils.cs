using Features.CharactersFeature.Components;
using UnityEngine;

public static class CharacterCommandUtils
{
    public static void ExecuteCommand(this CharacterComponent character, Command command)
    {
        if (character.IsDied)
            return;

        if (character.CurrentCommand != null)
        {
            character.CurrentCommand.Executed -= character.CommandExecuted;
            character.CurrentCommand.StopExecute();
        }

        character.CurrentCommand = command;
        command.BeginExecute(character);

        command.Executed += character.CommandExecuted;
    }

    private static void CommandExecuted(this CharacterComponent character, Command command)
    {
        Debug.Log($"{command.GetType().Name} executed by {character.Data.prototype.metaData.Name}");

        character.CurrentCommand.Executed -= character.CommandExecuted;
        character.CurrentCommand = null;
    }
}
