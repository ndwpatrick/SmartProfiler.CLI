using System;
using SmartProfiler.CLI.MemoryProfiler;

namespace SmartProfiler.CLI;

class Program
{
    private static string? memoryLogPath = null;
    private static bool useDetailedMemory = false;

    static void Main(string[] args)
    {
        Console.WriteLine("\n🚀 SmartProfiler CLI Started");

        // Parse CLI arguments
        foreach (var arg in args)
        {
            switch (arg)
            {
                case "--memory-log" or "-m" when args is [.., var path]:
                    memoryLogPath = path;
                    break;
                case "--detailed-memory" or "-d":
                    useDetailedMemory = true;
                    break;
            }
        }

        MemoryProfilerTool? memoryProfiler = null;

        if (!string.IsNullOrEmpty(memoryLogPath))
        {
            Console.WriteLine($"Memory profiling enabled. Report will be saved to: {memoryLogPath}");
            memoryProfiler = new MemoryProfilerTool(memoryLogPath, useDetailedMemory);
            memoryProfiler.Start();
        }

        if (memoryProfiler != null)
        {
            var result = memoryProfiler.Stop();
            Console.WriteLine(result.ToReadableReport());
        }

        Console.WriteLine("✅ Profiling Completed.\n");
    }
}
