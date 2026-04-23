using System.CommandLine;
using System.Reflection;

var rootCommand = new RootCommand("terbin-cli");
rootCommand.SetAction(Debug.Help);

Assembly.GetExecutingAssembly()
    .GetTypes()
    .Where(t => t.IsSubclassOf(typeof(TerbinCommand)) && !t.IsAbstract && t.GetCustomAttribute<IgnoreRootCommand>() == null)
    .ToList()
    .ForEach(t =>
    {
        if (Activator.CreateInstance(t) is TerbinCommand command)
            rootCommand.Add(command.Build());
    });

return rootCommand.Parse(args).Invoke();
