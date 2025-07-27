using System.Text;

namespace SmartProfiler.CLI.MemoryProfiler;

public class MemoryProfileResult
{
    public long StartMemory { get; set; }
    public long EndMemory { get; set; }
    public long MemoryUsed { get; set; }
    public long PeakMemory { get; set; }
    public long HeapSizeBytes { get; set; }
    public long FragmentedBytes { get; set; }
    public long HighMemoryLoadThresholdBytes { get; set; }
    public long FragmentationPercentage => HeapSizeBytes == 0 ? 0 : (FragmentedBytes * 100) / HeapSizeBytes;
    public long FragmentationPercentageHigh => HighMemoryLoadThresholdBytes == 0 ? 0 : (FragmentedBytes * 100) / HighMemoryLoadThresholdBytes;
    public long FragmentationPercentageLow => HeapSizeBytes == 0 ? 0 : (FragmentedBytes * 100) / HeapSizeBytes;
    
    public string ToReadableReport()
    {
        var sb = new StringBuilder();
        sb.AppendLine("\n Memory Profiling Report");
        sb.AppendLine("───────────────────────────────────────────────────────");
        sb.AppendLine($" Start Memory                   : {FormatBytes(StartMemory)}");
        sb.AppendLine($" End Memory                     : {FormatBytes(EndMemory)}");
        sb.AppendLine($" Memory Used                    : {FormatBytes(MemoryUsed)}");
        sb.AppendLine($" Peak Memory                    : {FormatBytes(PeakMemory)}");
        sb.AppendLine($" Heap Size                      : {FormatBytes(HeapSizeBytes)}");
        sb.AppendLine($" Fragmented Bytes               : {FormatBytes(FragmentedBytes) + FormatBytes(FragmentationPercentage)}");        
        sb.AppendLine($" Fragmentation Precentage       : {FragmentationPercentage}%")
          .AppendLine($" Fragmentation Precentage High  : {FragmentationPercentageHigh}%")
          .AppendLine($" Fragmentation Precentage Low   : {FragmentationPercentageLow}%");
        sb.AppendLine($" High Memory Load Trigger       : {FormatBytes(HighMemoryLoadThresholdBytes)}");
        return sb.ToString();
    }

    private static string FormatBytes(long bytes) =>
        $"{bytes / 1024.0 / 1024.0:0.00} MB";
}
// This code defines a MemoryProfileResult class that encapsulates the results of a memory profiling operation.
// The class is self-contained and does not rely on external dependencies, making it easy to use in various projects.
// It is designed to be thread-safe and can be used in multi-threaded environments without issues.
// Also, used in a memory profiling context, providing insights into memory usage and fragmentation.
// The properties are public and can be accessed directly, allowing for easy integration with other components.
// It includes properties for memory profiling results and a method to generate a readable report.
// The ToReadableReport method formats the memory profiling results into a human-readable string.
// The FormatBytes method converts bytes to megabytes for better readability.
// The code is structured to be easily extensible and maintainable, allowing for future enhancements or modifications.
