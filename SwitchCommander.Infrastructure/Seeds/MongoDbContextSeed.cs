using MongoDB.Driver;
using SwitchCommander.Domain.Dtos;
using SwitchCommander.Infrastructure.Context;

namespace SwitchCommander.Infrastructure.Seeds;

public class MongoDbContextSeed
{
    public MongoDbContextSeed(MongoDbSshContext sshContext)
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

        var hasInsert = sshContext.SSHCommandConfigurationCollection.FindAsync(x => true).Result.Any();
        if (!hasInsert)
            sshContext.SSHCommandConfigurationCollection.InsertOne(new SShCommandConfiguration
            {
                CommandMinimumLength = 1,
                DescriptionMinimumLength = 10,
                CommandMaximumLength = 200,
                DescriptionMaximumLength = 1000
            });
    }
}