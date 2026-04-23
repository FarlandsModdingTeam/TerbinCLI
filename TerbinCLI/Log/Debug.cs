using System.CommandLine;
using System.CommandLine.Invocation;
using System.Drawing;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Pastel;

public static class Debug
{
    public static void Log(TerbinCommand tc, string text)
    {
        var commandKey = tc.commandKey;

        Console.WriteLine($"[{commandKey.Pastel(Color.Orange)}] {text}");
    }
    public static void Help(ParseResult parseResult) => Help(parseResult.CommandResult.Command);
    public static void Help(Command command)
    {
        Console.WriteLine($"Description: {command.Description}");
        Console.WriteLine();

        if (command.Arguments.Any())
        {
            Console.WriteLine("Argument:");
            foreach (var arg in command.Arguments)
            {
                Console.WriteLine($"  {arg.Name.Pastel(Color.Orange)}\n\t{arg.Description}");
            }
            Console.WriteLine();
        }

        if (command.Options.Any())
        {
            Console.WriteLine("Options:");
            foreach (var option in command.Options)
            {
                List<string> names = [option.Name];
                names.AddRange(option.Aliases);

                Console.WriteLine($"  {string.Join(", ", names.Select(a => a.Pastel(Color.Orange)))}\n\t{option.Description}");
            }
            Console.WriteLine();
        }

        if (command.Subcommands.Any())
        {
            Console.WriteLine("Subcommands:");
            foreach (var sub in command.Subcommands)
            {
                Console.WriteLine($"  {sub.Name.Pastel(Color.Orange)} - {sub.Description}");
            }
        }
    }

    public static Option<bool> HelpOption => new Option<bool>("-h", "--help", "-?")
    {
        Description = "Show help information",
    };
}