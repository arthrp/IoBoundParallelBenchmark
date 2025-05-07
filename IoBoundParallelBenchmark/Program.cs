using BenchmarkDotNet.Running;

namespace IoBoundParallelBenchmark;

class Program
{
    static void Main(string[] args)
    {
        BenchmarkRunner.Run<LongJobBenchmark>();
    }
}