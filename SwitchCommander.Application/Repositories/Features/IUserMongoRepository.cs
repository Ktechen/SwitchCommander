using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Repositories.Features;

public interface IUserMongoRepository : IBaseMongoRepository<User>
{
    Task<User?> GetByEmail(string email, CancellationToken cancellationToken);
}