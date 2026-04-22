using System.CommandLine;
using System.Reflection;

[AttributeUsage(AttributeTargets.Class)]
public class IgnoreRootCommand : Attribute
{
}

public abstract class TerbinCommand
{
    public TerbinCommand? parent;
    public abstract string alias { get; }
    public abstract string description { get; }

    public string commandKey => parent != null ? parent.alias + "/" + alias : alias;

    public Command Build()
    {
        var command = new Command(alias, description);

        foreach (var field in this.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).OrderBy(f => f.Name))
        {
            if (field.FieldType.IsSubclassOf(typeof(Option)))
                command.Options.Add((Option)field.GetValue(this)!);

            else if (field.FieldType.IsSubclassOf(typeof(Argument)))
                command.Arguments.Add((Argument)field.GetValue(this)!);

            else if (field.FieldType.IsSubclassOf(typeof(Command)))
                command.Add((Command)field.GetValue(this)!);

            else if (field.FieldType.IsSubclassOf(typeof(TerbinCommand)))
            {
                var tc = (TerbinCommand)field.GetValue(this)!;
                tc.parent = this;
                command.Add(tc.Build());
            }
        }

        command.SetAction(OnExecute);

        return command;
    }


    public abstract void OnExecute(ParseResult parseResult);
    
    public void Log(string text) => Debug.Log(this, text);
}