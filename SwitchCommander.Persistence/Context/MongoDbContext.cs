using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Persistence.Context;

public class MongoDbContext 
{
    public IMongoCollection<User> UserCollection { get; }
    
    public IMongoCollection<Switch> SwitchCollection { get; }
    
    public MongoDbContext(IConfiguration configuration)
    {
        var config = configuration.GetSection("MongoDB");
        var mongoClient = new MongoClient(config.GetValue<string>("ConnectionString"));
        var mongoDatabase = mongoClient.GetDatabase(config.GetValue<string>("DatabaseName"));

        UserCollection = mongoDatabase.GetCollection<User>(nameof(User));
        SwitchCollection = mongoDatabase.GetCollection<Switch>(nameof(Switch));
    }

}