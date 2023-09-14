using Microsoft.EntityFrameworkCore;
using SwitchCommander.Application.Repositories;
using SwitchCommander.Domain.Dtos;
using SwitchCommander.Persistence.Context;

namespace SwitchCommander.Persistence.Repositories;

public class UserRepository : BaseRepository<UserDto>, IUserRepository
{
    public UserRepository(DataContext context) : base(context)
    {
    }
    
    public Task<UserDto?> GetByEmail(string email, CancellationToken cancellationToken)
    {
        return Context.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }
}