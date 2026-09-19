namespace SmartProfiler.CLI.Application.Profilers.CPU;

public sealed record CpuProfileResult(
    string MethodName,
    TimeSpan CpuTime,
    TimeSpan ElapsedTime,
    double CpuUsagePercentage,
    int LogicalProcessorCount);
