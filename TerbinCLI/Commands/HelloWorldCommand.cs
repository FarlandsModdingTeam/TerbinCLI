using System.CommandLine;
using System.Drawing;
using Newtonsoft.Json;
using Pastel;
public class HelloWorldCommand : TerbinCommand
{
    public override string alias => "hello";

    public override string description => "Realiza un Hello World!";

    [OptionAttr(Name = "--name" )]
    private Option<string> NameOption = null!; 

    public override void OnExecute(ParseResult parseResult)
    {
        var name = parseResult.GetValue(NameOption);   
        
        Console.WriteLine($"{"[terbin]".Pastel(Color.Orange)} Hello {name ?? "World"}");
    }
}