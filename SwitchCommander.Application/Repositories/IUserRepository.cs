using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByEmail(string email, CancellationToken cancellationToken);
}