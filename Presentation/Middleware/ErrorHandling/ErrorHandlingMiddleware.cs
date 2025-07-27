using Spectre.Console;
using Spectre.Console.Cli;

namespace Profilers.ErrorHandling;

public class ErrorHandlingMiddleware : ICommandInterceptor
{
    public void Intercept(CommandContext context, CommandSettings settings)
    {
        AnsiConsole.MarkupLineInterpolated($"[gray]Intercepting command: {context.Name} at {DateTime.Now}[/]");
    }
}