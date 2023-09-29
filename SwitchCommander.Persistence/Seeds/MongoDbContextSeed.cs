using SwitchCommander.Application.Repositories.Features;
using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Persistence.Seeds;

public class MongoDbContextSeed
{
    public MongoDbContextSeed(ISSHCommanderRepository repository)
    {
        var seed = new List<SSHCommand>
        {
            new()
            {
                Id = new Guid(),
                Hash = new Guid().ToString(),
                Command = "ls"
            },
            new()
            {
                Id = new Guid(),
                Hash = new Guid().ToString(),
                Command = "show version"
            }
        };
        repository.AddRangeAsync(seed);
    }
}