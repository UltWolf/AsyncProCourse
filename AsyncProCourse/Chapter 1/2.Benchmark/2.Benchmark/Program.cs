using _2.Benchmark.Benchmarks;
using BenchmarkDotNet.Running;

namespace _2.Benchmark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<MD5vsSHA256>();
        }
    }
}