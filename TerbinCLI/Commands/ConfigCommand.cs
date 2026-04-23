using System.CommandLine;
using System.Drawing;
using Pastel;

public class ConfigCommand : TerbinCommand
{
    public override string alias => "config";
    public override string description => "Modify configuration";

    public ConfigSetCommand configCommand = new();
    public override void OnExecute(ParseResult parseResult)
    {
        // Simula la ejecución del comando con --help
        Debug.Help(parseResult.CommandResult.Command);
    }

}

[IgnoreRootCommand]
public class ConfigSetCommand : TerbinCommand
{
    public override string alias => "set";
    public override string description => "Set a value";

    private Argument<string> a0_key = new Argument<string>("Key");
    private Argument<string> a1_value = new Argument<string>("Value");

    public override void OnExecute(ParseResult parseResult)
    {
        var key = parseResult.GetValue(a0_key);
        var value = parseResult.GetValue(a1_value);

        Log($"{key} {value}");
    }
}