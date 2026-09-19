using System.Diagnostics;
using SmartProfiler.CLI.Application.Profilers.CPU;

namespace SmartProfiler.CLI.Core.Diagnostics;

public class MethodCpuAnalyzer
{
    public MethodCpuMeasurement Start()
    {
        var process = Process.GetCurrentProcess();
        return new MethodCpuMeasurement(process.TotalProcessorTime, Stopwatch.GetTimestamp());
    }

    public CpuProfileResult Stop(string methodName, MethodCpuMeasurement start)
    {
        var process = Process.GetCurrentProcess();
        var cpuTime = process.TotalProcessorTime - start.CpuTime;
        var elapsedTime = Stopwatch.GetElapsedTime(start.Timestamp);
        var processorCount = Environment.ProcessorCount;
        var usage = elapsedTime.TotalMilliseconds <= 0
            ? 0
            : cpuTime.TotalMilliseconds / (elapsedTime.TotalMilliseconds * processorCount) * 100;

        return new CpuProfileResult(
            methodName,
            cpuTime,
            elapsedTime,
            usage,
            processorCount);
    }
}

public readonly record struct MethodCpuMeasurement(TimeSpan CpuTime, long Timestamp);
