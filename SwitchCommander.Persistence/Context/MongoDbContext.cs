using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Persistence.Context;

public class MongoDbContext
{
    private IConfiguration _configuration;
    
    public IMongoCollection<User> UserCollection { get; set; }
    
    public MongoDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        var config = configuration.GetSection("MongoDB");
        var mongoClient = new MongoClient(config.GetValue<string>("ConnectionString"));
        var mongoDatabase = mongoClient.GetDatabase(config.GetValue<string>("DatabaseName"));

        UserCollection = mongoDatabase.GetCollection<User>(nameof(User));
    }

}