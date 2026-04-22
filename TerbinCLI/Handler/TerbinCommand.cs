using System.CommandLine;
using System.Reflection;

[AttributeUsage(AttributeTargets.Field)]
public class OptionAttr : Attribute
{
    public required string Name;
    public string[]? Alias;
    public bool? Required;
}

public abstract class TerbinCommand
{
    public abstract string alias { get; }
    public abstract string description { get; }

    public Command Build()
    {
        var command = new Command(alias, description);

        foreach (var field in this.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
        {
            if (field.GetType().IsSubclassOf(typeof(Option)))
                continue;

            command.Options.Add((Option)field.GetValue(this)!);
        }

        command.SetAction(OnExecute);

        return command;
    }


    public abstract void OnExecute(ParseResult parseResult);
}