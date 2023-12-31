﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Security.Cryptography;

namespace _2.Benchmark.Benchmarks
{
    [RPlotExporter]
    [CsvMeasurementsExporter]
    [JsonExporter]
    //Just which version of framework we need to use for test
    [SimpleJob(runtimeMoniker: RuntimeMoniker.Net70)]
    [SimpleJob(runtimeMoniker: RuntimeMoniker.Net50)]
    //[SimpleJob(runtimeMoniker: RuntimeMoniker.NetCoreApp20)]
    //[SimpleJob(runtimeMoniker: RuntimeMoniker.NetCoreApp21)]
    // for finding memory allocations, if we want to find leaks, need to use NativeMemoryDiagnoser
    [MemoryDiagnoser]
    //For find some lock problem, and completed work items, support from 3.0
    [ThreadingDiagnoser]
    public class MD5vsSHA256
    {
        private SHA256 SHA256 = SHA256.Create();
        private MD5 md5 = MD5.Create();
        private byte[] data;
        [GlobalSetup]
        public void Setup()
        {

            data = new byte[1000];
            new Random(42).NextBytes(data);
        }
        [Benchmark]
        public void Sha256()
        {
            var count = 10;
            var locked = new object();
            using var e = new CountdownEvent(count);
            for (var i = 0; i < count; i++)
            {
                ThreadPool.QueueUserWorkItem(m =>
                {
                    lock (locked)
                    {
                        // SHA256.ComputeHash(data);

                        ((CountdownEvent)m).Signal();
                    }
                });
            }
            e.Wait();

        }
        [Benchmark]
        public void Md5()
        {
            var count = 10;
            var locked = new object();
            using var e = new CountdownEvent(count);
            for (var i = 0; i < count; i++)
            {
                ThreadPool.QueueUserWorkItem(m =>
                {
                    lock (locked)
                    {
                        //md5.ComputeHash(data);
                        ((CountdownEvent)m).Signal();
                    }
                });
            }
            e.Wait();
        }
    }


}
