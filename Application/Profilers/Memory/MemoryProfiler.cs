using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

using  SmartProfiler.CLI.MemoryProfiler;

public class MemoryProfilerTool
{
    private long _startMemory;
    private long _peakMemory;
    private readonly bool _useDetailedMemory;
    private Timer _timer;
    private string? _logFilePath;

    public MemoryProfilerTool(string? logFilePath = null, bool? useDetailedMemory = false)
    {
        _peakMemory = 0;
        _logFilePath = logFilePath;
        _useDetailedMemory = useDetailedMemory ?? false;
        _timer = new Timer(UpdatePeakMemory, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(100));
    }

    public void Start()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        _startMemory = GC.GetTotalMemory(true);
    }

    public MemoryProfileResult Stop()
    {
        _timer.Dispose();

        long endMemory = GC.GetTotalMemory(true);
        var gcInfo = GC.GetGCMemoryInfo();

        if (_useDetailedMemory)
        {
            ProfileMemory();
        }

        var result = new MemoryProfileResult
        {
            StartMemory = _startMemory,
            EndMemory = endMemory,
            MemoryUsed = endMemory - _startMemory,
            PeakMemory = _peakMemory,
            HeapSizeBytes = gcInfo.HeapSizeBytes,
            FragmentedBytes = gcInfo.FragmentedBytes,
            HighMemoryLoadThresholdBytes = gcInfo.HighMemoryLoadThresholdBytes
        };

        if (!string.IsNullOrEmpty(_logFilePath))
        {
            File.WriteAllText(_logFilePath, result.ToReadableReport());
        }

        return result;
    }

    public void ProfileMemory()
    {
        // Generate a timestamped log file name if no log file path is provided
        if (string.IsNullOrEmpty(_logFilePath))
        {
            var timestamp = DateTime.Now.ToString("yyyyMMdd-HHmm");
            var defaultLogDirectory = Path.Combine(Directory.GetCurrentDirectory(), "logs");
            Directory.CreateDirectory(defaultLogDirectory); // Ensure the /logs folder exists
            _logFilePath = Path.Combine(defaultLogDirectory, $"memory-{timestamp}.txt");
        }
        else
        {
            // Ensure the directory for the provided log file path exists
            var logDirectory = Path.GetDirectoryName(_logFilePath);
            if (!string.IsNullOrEmpty(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
        }

        using var writer = new StreamWriter(_logFilePath, append: false);
        writer.AutoFlush = true;

        var totalMemory = GC.GetTotalMemory(forceFullCollection: false);
        Console.WriteLine($"[MemoryProfiler] Total Memory: {totalMemory / 1024.0:N2} KB");

        var info = GC.GetGCMemoryInfo();
        Console.WriteLine("\n[Detailed Memory Profiling]");
        Console.WriteLine($"  High Memory Load Threshold: {info.HighMemoryLoadThresholdBytes / 1024.0:N2} KB");
        Console.WriteLine($"  Total Available Memory:     {info.TotalAvailableMemoryBytes / 1024.0:N2} KB");
        Console.WriteLine($"  Heap Size Bytes:            {info.HeapSizeBytes / 1024.0:N2} KB");
        Console.WriteLine($"  Fragmented Bytes:           {info.FragmentedBytes / 1024.0:N2} KB");
        double memoryLoadPercent = (double)info.HeapSizeBytes / info.TotalAvailableMemoryBytes * 100;
        Console.WriteLine($"  Approx. Memory Load:        {memoryLoadPercent:N2}%");
        Console.WriteLine($"  Finalization Pending:       {GC.GetGCMemoryInfo().FinalizationPendingCount}");
        Console.WriteLine($"  Pinned Objects:             [Approximation] — Not directly exposed but can be inferred.");
        Console.WriteLine($"  SOH / LOH breakdown is internal but HeapSizeBytes gives a rough idea.");
    }

    private void UpdatePeakMemory(object? state)
    {
        long current = Process.GetCurrentProcess().PrivateMemorySize64;
        if (current > _peakMemory)
            _peakMemory = current;
    }
}
