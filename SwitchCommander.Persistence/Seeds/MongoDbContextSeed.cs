using MongoDB.Driver;
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
                Name = new Guid().ToString(),
                Command = "ls"
            },
            new()
            {
                Id = new Guid(),
                Name = new Guid().ToString(),
                Command = "show version"
            }
        };
        
        var hasInsert = context.SSHCommandConfigurationCollection.FindAsync(x => true).Result.Any();
        if (!hasInsert)
        {
            context.SSHCommandConfigurationCollection.InsertOne(new SShCommandConfiguration
            {
                CommandMinimumLength = 1,
                DescriptionMinimumLength = 10,
                CommandMaximumLength = 200,
                DescriptionMaximumLength = 1000
            });
        }
    }
}