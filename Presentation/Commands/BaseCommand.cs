using Spectre.Console;
using Spectre.Console.Cli;

public abstract class BaseCommand<TSettings> : Command<TSettings> where TSettings : CommandSettings
{
    protected IAnsiConsole _console { get; }

    protected BaseCommand()
    {
        _console = AnsiConsole.Console;
    }

    protected void LogStart(string message)
    {
        _console.MarkupLine($"[blue][[Start]][/] {message}");
    }

    protected void LogSuccess(string message)
    {
        _console.MarkupLine($"[green][[Success]][/] {message}");
    }

    protected void LogError(string message)
    {
        _console.MarkupLine($"[red][[Error]][/] {message}");
    }

    protected void LogWarning(string message)
    {
        _console.MarkupLine($"[yellow][[Warning]][/] {message}");
    }

    protected void LogInfo(string message)
    {
        _console.MarkupLine($"[grey][[Info]][/] {message}");
    }
}