namespace SmartProfiler.CLI.Core.Interfaces;

public interface IProfilerEngine
{
    Task ExecuteProfiledMethodAsync(string? methodName, string? assemblyPath, object[]? methodParams = null);
}