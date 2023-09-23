using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using SwitchCommander.Benchmark;

var config = ManualConfig
    .Create(DefaultConfig.Instance)
    .WithOptions(ConfigOptions.DisableOptimizationsValidator);

//BenchmarkRunner.Run<BenchmarkWebApi>(config);