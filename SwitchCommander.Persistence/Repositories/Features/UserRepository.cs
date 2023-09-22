using MongoDB.Driver;
using SwitchCommander.Application.Repositories.Features;
using SwitchCommander.Domain.Dtos;
using SwitchCommander.Persistence.Context;

namespace SwitchCommander.Persistence.Repositories.Features;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(MongoDbContext context) : base(context.UserCollection)
    {
    }

    public async Task<User?> GetByEmail(string email, CancellationToken cancellationToken)
    {
        return await Collection.Find(x => x.Email == email).FirstAsync(cancellationToken);
    }
}