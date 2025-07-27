using System.Diagnostics;

namespace SmartProfiler.Profilers;

public class CpuProfiler
{
    public static async Task ProfileAsync(Func<Task> methodToProfile, bool logToConsole = true)
    {
        var process = Process.GetCurrentProcess();

        TimeSpan cpuBefore = process.TotalProcessorTime;
        DateTime wallClockStart = DateTime.UtcNow;

        await methodToProfile();

        DateTime wallClockEnd = DateTime.UtcNow;
        TimeSpan cpuAfter = process.TotalProcessorTime;
        TimeSpan cpuUsed = cpuAfter - cpuBefore;
        TimeSpan wallClockTime = wallClockEnd - wallClockStart;
        int logicalProcessors = Environment.ProcessorCount;
        double cpuUsagePercent = (cpuUsed.TotalMilliseconds / (wallClockTime.TotalMilliseconds * logicalProcessors)) * 100;

        if (logToConsole)
        {
            Console.WriteLine("\n🧠 CPU Usage Breakdown");
            Console.WriteLine("----------------------");
            Console.WriteLine($"Total CPU Time (User + Kernel): {cpuUsed}");
            Console.WriteLine($"Wall Clock Time: {wallClockTime}");
            Console.WriteLine($"Logical Processors: {logicalProcessors}");
            Console.WriteLine($"Approx. CPU Usage: {cpuUsagePercent:F2}%");
        }
    }
}