using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;
using SmartProfiler.CLI.Core.Interfaces;

namespace SmartProfiler.CLI.Presentation.Commands;

public class ProfileCommand : AsyncCommand<ProfileSettings>
{
    private readonly IProfilerEngine _profilerEngine;

    public ProfileCommand(IProfilerEngine profilerEngine)
    {
        _profilerEngine = profilerEngine;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, ProfileSettings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.ProcessName))
        {
            AnsiConsole.MarkupLine("[red]Error:[/] You must provide a process name to profile.");
            return -1;
        }

        AnsiConsole.MarkupLine("[bold green]Starting profiling...[/]");
        await _profilerEngine.ExecuteProfiledMethodAsync(settings.ProcessName, null, null);
        AnsiConsole.MarkupLine("[bold green]Profiling complete.[/]");

        return 0;
    }
}