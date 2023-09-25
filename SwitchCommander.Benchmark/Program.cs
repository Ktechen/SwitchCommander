using BenchmarkDotNet.Configs;

var config = ManualConfig
    .Create(DefaultConfig.Instance)
    .WithOptions(ConfigOptions.DisableOptimizationsValidator);

//BenchmarkRunner.Run<BenchmarkWebApi>(config);