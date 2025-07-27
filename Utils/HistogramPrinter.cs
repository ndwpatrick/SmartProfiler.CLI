namespace SmartProfiler.CLI;

public static class HistogramPrinter
{
    public static void PrintHistogram(List<double> values, string metric)
    {
        Console.WriteLine($"\n{metric} Distribution:");

        double min = values.Min();
        double max = values.Max();
        double range = max - min;

        int bucketCount = 10;
        double bucketSize = range / bucketCount;

        if (bucketSize == 0)
        {
            Console.WriteLine("All values are identical, no distribution to show.");
            return;
        }

        int[] buckets = new int[bucketCount];

        foreach (var value in values)
        {
            int bucketIndex = (int)((value - min) / bucketSize);
            if (bucketIndex == bucketCount) bucketIndex--; // handle max edge case
            buckets[bucketIndex]++;
        }

        for (int i = 0; i < bucketCount; i++)
        {
            double rangeStart = min + i * bucketSize;
            double rangeEnd = rangeStart + bucketSize;
            Console.Write($"{rangeStart:F2}-{rangeEnd:F2}: ");
            Console.WriteLine(new string('*', buckets[i]));
        }
    }
}