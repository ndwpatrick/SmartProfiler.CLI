using SmartProfiler.CLI.Core.Interfaces;
using SmartProfiler.CLI.Infrastructure.Diagnostics;
using Spectre.Console;

namespace SmartProfiler.CLI.Application.Profilers.Common;

public class ProcessProfilerEngine : IProfilerEngine
{
    private readonly IAnsiConsole _console;
    private readonly ICpuDiagnostics _cpuDiagnostics;

    public ProcessProfilerEngine(IAnsiConsole console, ICpuDiagnostics cpuDiagnostics)
    {
        _console = console;
        _cpuDiagnostics = cpuDiagnostics;
    }

    public async Task ExecuteProfiledMethodAsync(string? methodName, string? assemblyPath, object[]? methodParams = null)
    {
        if (string.IsNullOrWhiteSpace(methodName))
        {
            _console.MarkupLine("[red]Error:[/] Process name (methodName) must be provided.");
            return;
        }

        await Task.Run(() =>
        {
            _console.MarkupLine($"[bold yellow]Starting profiling for process:[/] {methodName}");

            var report = _cpuDiagnostics.GetCpuUsagePercentage(methodName);

            _console.WriteLine();
            _console.MarkupLine("[green]Profiling completed. Results:[/]");
            _console.WriteLine();
            _console.Write(report.ToString());
        });
    }
}