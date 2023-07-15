using _2.Benchmark.Configs;
using BenchmarkDotNet.Attributes;
using YamlDotNet.Serialization;

namespace _2.Benchmark.Benchmarks
{
    [Config(typeof(Config))]
    public class NugetSample
    {

        private readonly ISerializer serializer = new SerializerBuilder().Build();
        [Benchmark]
        public string Serialize() => serializer.Serialize(new { price = 2, product = 1, name = "Bublya" });

    }
}
