using SwitchCommander.Domain.Dtos;
using SwitchCommander.Persistence.Context;

namespace SwitchCommander.Persistence.Seeds;

public class MongoDbContextSeed
{
    public MongoDbContextSeed(MongoDbContext context)
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
        context.SSHCommandCollection.InsertMany(seed);
    }
}