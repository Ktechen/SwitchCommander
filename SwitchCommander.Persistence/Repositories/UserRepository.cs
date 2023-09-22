using MongoDB.Driver;
using SwitchCommander.Application.Repositories;
using SwitchCommander.Domain.Dtos;
using SwitchCommander.Persistence.Context;

namespace SwitchCommander.Persistence.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private MongoDbContext _context;
    
    public UserRepository(MongoDbContext context) : base(context.UserCollection)
    {
        _context = context;
    }
    public async Task<User?> GetByEmail(string email, CancellationToken cancellationToken)
    {
        return await Collection.Find(x => x.Email == email).FirstAsync(cancellationToken);
    }



}