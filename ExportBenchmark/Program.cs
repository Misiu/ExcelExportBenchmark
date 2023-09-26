using BenchmarkDotNet.Running;

namespace ExportBenchmark
{
    internal class Program
    {
        static void Main()
        {
            //run benchmark only once
            var summary = BenchmarkRunner.Run<Benchmarks>();

            //BenchmarkRunner.Run<Benchmarks>();
        }
    }
}