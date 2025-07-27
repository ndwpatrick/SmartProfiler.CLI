using System.Diagnostics;

namespace SmartProfiler.CLI;

public static class SmartProfiler
{
    private const double RegressionThreshold = 0.2; // 20%

    public static void Measure(Action actionToProfile, int runs = 10, string label = "Execution", string? csvPath = null)
    {
        var timeResults = new List<double>();
        var memoryResults = new List<double>();

        for (int i = 0; i < runs; i++)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            var stopwatch = Stopwatch.StartNew();
            long beforeMemory = GC.GetTotalMemory(true);

            actionToProfile.Invoke();

            stopwatch.Stop();
            long afterMemory = GC.GetTotalMemory(false);

            double elapsedMs = stopwatch.Elapsed.TotalMilliseconds;
            double usedMemoryKb = (afterMemory - beforeMemory) / 1024.0;

            timeResults.Add(elapsedMs);
            memoryResults.Add(usedMemoryKb);
        }

        PrintStats(timeResults, memoryResults, label);
        HistogramPrinter.PrintHistogram(timeResults, "Time (ms)");
        HistogramPrinter.PrintHistogram(memoryResults, "Memory (KB)");
        DetectPerformanceRegression(timeResults);
        AnalyzeBottleneck(timeResults, memoryResults);

        if (!string.IsNullOrEmpty(csvPath))
        {
            CsvExporter.ExportToCsvFile(timeResults, memoryResults, csvPath);
            Console.WriteLine($"\n=> Results exported to: {csvPath}");
        }
    }

    public static async Task MeasureAsync(Func<Task> asyncActionToProfile, int runs = 10, string label = "Execution", string? csvPath = null)
    {
        var timeResults = new List<double>();
        var memoryResults = new List<double>();

        for (int i = 0; i < runs; i++)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            var stopwatch = Stopwatch.StartNew();
            long beforeMemory = GC.GetTotalMemory(true);

            await asyncActionToProfile.Invoke();

            stopwatch.Stop();
            long afterMemory = GC.GetTotalMemory(false);

            double elapsedMs = stopwatch.Elapsed.TotalMilliseconds;
            double usedMemoryKb = (afterMemory - beforeMemory) / 1024.0;

            timeResults.Add(elapsedMs);
            memoryResults.Add(usedMemoryKb);
        }

        PrintStats(timeResults, memoryResults, label);
        HistogramPrinter.PrintHistogram(timeResults, "Time (ms)");
        HistogramPrinter.PrintHistogram(memoryResults, "Memory (KB)");
        DetectPerformanceRegression(timeResults);
        AnalyzeBottleneck(timeResults, memoryResults);

        if (!string.IsNullOrEmpty(csvPath))
        {
            CsvExporter.ExportToCsvFile(timeResults, memoryResults, csvPath);
            Console.WriteLine($"\n=> Results exported to: {csvPath}");
        }
    }

    private static void PrintStats(List<double> times, List<double> memories, string label)
    {
        Console.WriteLine($"\n=== [{label}] Stats ===");
        Console.WriteLine($"Runs: {times.Count}");
        Console.WriteLine($"Time (ms): Min = {times.Min():F2}, Max = {times.Max():F2}, Avg = {times.Average():F2}");
        Console.WriteLine($"Memory (KB): Min = {memories.Min():F2}, Max = {memories.Max():F2}, Avg = {memories.Average():F2}");
    }

    private static void DetectPerformanceRegression(List<double> times)
    {
        if (times.Count < 2)
            return;

        double avg = times.Average();
        double lastRun = times.Last();

        if (Math.Abs(lastRun - avg) / avg > RegressionThreshold)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n⚠️ WARNING: Performance Regression Detected!");
            Console.WriteLine($"Last run = {lastRun:F2} ms vs Avg = {avg:F2} ms");
            Console.ResetColor();
        }
    }

    private static void AnalyzeBottleneck(List<double> times, List<double> memories)
    {
        Console.WriteLine("\n🔎 Bottleneck Analysis:");

        double avgTime = times.Average();
        double avgMemory = memories.Average();

        if (avgTime > 1000) // over 1 second average per run
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=> Suggestion: CPU-bound or heavy synchronous processing detected. Optimize algorithm or parallelize work.");
            Console.ResetColor();
        }
        else if (avgMemory > 50000) // over ~50 MB average
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=> Suggestion: Memory-bound operation detected. Investigate data structures, caching, and object lifetimes.");
            Console.ResetColor();
        }
        else if (times.Max() - times.Min() > 500) // high variability
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=> Suggestion: Potential I/O-bound behavior. Consider async operations, batching, or retry strategies.");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=> No major bottleneck detected. Code appears efficient under current test conditions.");
            Console.ResetColor();
        }
    }
}
