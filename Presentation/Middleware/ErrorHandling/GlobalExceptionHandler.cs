using SmartProfiler.CLI.Presentation.Middleware.ErrorHandling;
using Spectre.Console;
using Spectre.Console.Cli;


namespace Profilers.ErrorHandling;

public class GlobalExceptionHandler : ICommandExceptionHandler
{
    public int OnException(CommandContext context, Exception ex)
    {
        AnsiConsole.MarkupLineInterpolated($"[red]A fatal error occurred in: {context.Name}[/]");
        AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything | ExceptionFormats.ShowLinks);
        return -99;
    }
}