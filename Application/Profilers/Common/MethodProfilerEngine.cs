using System.Diagnostics;
using System.Reflection;
using SmartProfiler.CLI.Core.Interfaces;
using Spectre.Console;

namespace SmartProfiler.Profilers;

public class MethodProfilerEngine : IProfileEnricher
{
    public string Name => "MethodProfiler";

    public async Task ExecuteProfiledMethodAsync(string? methodName, string? assemblyPath, object[]? methodParams = null)
    {
        if (string.IsNullOrWhiteSpace(methodName) || string.IsNullOrWhiteSpace(assemblyPath))
        {
            AnsiConsole.MarkupLine("[red]Error:[/] Method name and assembly path must be provided.");
            return;
        }

        if (!File.Exists(assemblyPath))
        {
            AnsiConsole.MarkupLine($"[red]Error:[/] Assembly not found at path: [yellow]{assemblyPath}[/]");
            return;
        }

        try
        {
            var assembly = Assembly.LoadFrom(assemblyPath);
            var lastDot = methodName.LastIndexOf('.');
            if (lastDot < 0)
            {
                AnsiConsole.MarkupLine("[red]Error:[/] Invalid method format. Use [green]Namespace.Class.Method[/].");
                return;
            }

            var typeName = methodName[..lastDot];
            var methodShortName = methodName[(lastDot + 1)..];

            var type = assembly.GetType(typeName);
            if (type == null)
            {
                AnsiConsole.MarkupLine($"[red]Error:[/] Type [yellow]{typeName}[/] not found in the assembly.");
                return;
            }

            var method = type.GetMethod(methodShortName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            if (method == null)
            {
                AnsiConsole.MarkupLine($"[red]Error:[/] Method [yellow]{methodShortName}[/] not found in type [green]{typeName}[/].");
                return;
            }

            object? instance = null;
            if (!method.IsStatic)
            {
                instance = Activator.CreateInstance(type);
                if (instance == null)
                {
                    AnsiConsole.MarkupLine($"[red]Error:[/] Failed to create instance of type [green]{typeName}[/].");
                    return;
                }
            }

            var sw = Stopwatch.StartNew();
            var result = method.Invoke(instance, methodParams);
            sw.Stop();

            if (result is Task task)
            {
                await task;

                if (result.GetType().IsGenericType)
                {
                    var resultProperty = result.GetType().GetProperty("Result");
                    var awaitedResult = resultProperty?.GetValue(result);
                    AnsiConsole.MarkupLine($"[blue]Async result:[/] [green]{awaitedResult}[/]");
                }
            }
            else if (result != null)
            {
                AnsiConsole.MarkupLine($"[blue]Result:[/] [green]{result}[/]");
            }

            AnsiConsole.Write(new Rule("[green]Method Profiling Complete[/]").RuleStyle("grey").Centered());
            AnsiConsole.MarkupLine($"[bold]Method:[/] [cyan]{methodName}[/]");
            AnsiConsole.MarkupLine($"[bold]Execution Time:[/] [yellow]{sw.ElapsedMilliseconds} ms[/]");
        }
        catch (TargetInvocationException ex)
        {
            AnsiConsole.MarkupLine($"[red]Error:[/] Exception thrown by method: [italic]{ex.InnerException?.Message}[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error:[/] Unexpected error occurred: [italic]{ex.Message}[/]");
        }
    }

    public bool Enabled(IDictionary<string, string> environmentVariables)
    {
        return true; // Placeholder
    }

    public void Enrich(Profile profile)
    {
        // Placeholder logic
    }
}