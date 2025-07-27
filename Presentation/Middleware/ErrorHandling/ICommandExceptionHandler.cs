using Spectre.Console.Cli;

namespace SmartProfiler.CLI.Presentation.Middleware.ErrorHandling;

public interface ICommandExceptionHandler
{
    int OnException(CommandContext context, Exception ex);
}