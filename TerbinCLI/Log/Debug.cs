using System.Drawing;
using System.Net;
using Pastel;

public static class Debug
{
    public static void Log(TerbinCommand tc, string text)
    {
        var commandKey = tc.commandKey;

        Console.WriteLine($"[{commandKey.Pastel(Color.Orange)}] {text}");
    }
}