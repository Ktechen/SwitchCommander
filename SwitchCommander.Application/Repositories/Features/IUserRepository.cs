using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Repositories.Features;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByEmail(string email, CancellationToken cancellationToken);
}