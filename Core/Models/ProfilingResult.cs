namespace SmartProfiler.CLI.Core.Models;

public sealed class ProfilingResult
{
    public string Target { get; set; } = string.Empty;
    public long ElapsedMilliseconds { get; set; }
}