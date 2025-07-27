using System.Diagnostics;
using SmartProfiler.CLI.Infrastructure.Diagnostics;

namespace SmartProfiler.CLI.Infrastructure.Diagnostics;

public class CpuDiagnostics : ICpuDiagnostics
{
    public async Task<float> GetCpuUsagePercentage(string processName, int samplingDurationMs = 500)
    {
        try
        {
            var process = Process.GetProcessesByName(processName).FirstOrDefault();

            if (process == null)
            {
                throw new InvalidOperationException($"Process '{processName}' not found.");
            }

            var totalCpuTimeStart = process.TotalProcessorTime;
            var stopwatch = Stopwatch.StartNew();

            await Task.Delay(samplingDurationMs);

            stopwatch.Stop();
            var totalCpuTimeEnd = process.TotalProcessorTime;

            var cpuUsedMs = (totalCpuTimeEnd - totalCpuTimeStart).TotalMilliseconds;
            var elapsedMs = stopwatch.ElapsedMilliseconds;
            var cpuUsageTotal = cpuUsedMs / (elapsedMs * Environment.ProcessorCount) * 100;

            return (float)Math.Round(cpuUsageTotal, 2);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to get CPU usage: {ex.Message}");
            return -1f;
        }
    }
}