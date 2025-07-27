namespace SmartProfiler.CLI.Infrastructure.Diagnostics;

public interface ICpuDiagnostics
{
    Task<float> GetCpuUsagePercentage(string processName, int samplingDurationMs = 500);
}