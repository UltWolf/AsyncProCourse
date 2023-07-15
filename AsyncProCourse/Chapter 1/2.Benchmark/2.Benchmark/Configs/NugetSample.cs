using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;

namespace _2.Benchmark.Configs
{

    public class Config : ManualConfig
    {
        public Config()
        {
            AddDiagnoser(MemoryDiagnoser.Default);
            var baseJob = Job.MediumRun;
            AddJob(baseJob.WithNuGet("YamlDotNet", "7.0.0").WithId("7.0.0"));
            AddJob(baseJob.WithNuGet("YamlDotNet", "6.1.2").WithId("6.1.2"));
            AddJob(baseJob.WithNuGet("YamlDotNet", "5.4.0").WithId("5.4.0"));
        }
    }
}
