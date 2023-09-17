using ParkBee.MongoDb;
using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Persistence.Context;

public class MongoDbContext : MongoContext
{
    public MongoDbContext(IMongoContextOptionsBuilder optionsBuilder) : base(optionsBuilder)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override async void OnConfiguring()
    {
        OptionsBuilder.Entity<User>(entity => { entity.HasKey(p => p.Id); });
    }
}