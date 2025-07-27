namespace SmartProfiler.CLI;

public static class CsvExporter
{
    public static void ExportToCsvFile(List<double> times, List<double> memories, string csvPath)
    {
        using var writer = new StreamWriter(csvPath);

        writer.WriteLine("Run,Time (ms),Memory (KB)");

        for (int i = 0; i < times.Count; i++)
        {
            writer.WriteLine($"{i + 1},{times[i]:F2},{memories[i]:F2}");
        }
    }
}