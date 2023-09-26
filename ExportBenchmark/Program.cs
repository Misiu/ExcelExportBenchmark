using BenchmarkDotNet.Running;

namespace ExportBenchmark
{
    internal class Program
    {
        static void Main()
        {
            var summary = BenchmarkRunner.Run<Benchmarks>();
        }
    }
}