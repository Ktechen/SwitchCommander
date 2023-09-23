using BenchmarkDotNet.Attributes;
using SwitchCommander.WebAPI.Client;

namespace SwitchCommander.Benchmark;

[MemoryDiagnoser]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class BenchmarkWebApi
{
    
    
    [Benchmark]
    public void CreateOne()
    {
        var client = new UserClientService(new HttpClient());
        client.CreateAsync(new CreateUserRequest
        {
            Email = Faker.Internet.Email(),
            Name = Faker.Internet.UserName()
        });
    }
    
    [Benchmark]
    public void CreateTen()
    {
        var client = new UserClientService(new HttpClient());
        for (var i = 0; i <= 10; i++)
        {
            client.CreateAsync(new CreateUserRequest
            {
                Email = Faker.Internet.Email(),
                Name = Faker.Internet.UserName()
            });
        }
    }
    
    [Benchmark]
    public void CreateThousends()
    {
        var client = new UserClientService(new HttpClient());
        for (var i = 0; i <= 1_000; i++)
        {
            client.CreateAsync(new CreateUserRequest
            {
                Email = Faker.Internet.Email(),
                Name = Faker.Internet.UserName()
            });
        }
    }
    
    [Benchmark]
    public void CreateHundertThousend()
    {
        var client = new UserClientService(new HttpClient());
        for (var i = 0; i <= 1_000_00; i++)
        {
            client.CreateAsync(new CreateUserRequest
            {
                Email = Faker.Internet.Email(),
                Name = Faker.Internet.UserName()
            });
        }
    }
    
    [Benchmark]
    public void CreateMio()
    {
        var client = new UserClientService(new HttpClient());
        for (var i = 0; i <= 1_000_000; i++)
        {
            client.CreateAsync(new CreateUserRequest
            {
                Email = Faker.Internet.Email(),
                Name = Faker.Internet.UserName()
            });
        }
    }
}