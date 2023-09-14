using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Repositories;

public interface IUserRepository : IBaseRepository<UserDto>
{
    Task<UserDto?> GetByEmail(string email, CancellationToken cancellationToken);
}