namespace SwitchCommander.Blazor.Services;

public class UserService : IUserClient
{
    public Task<CreateUserResponse> CreateAsync(CreateUserRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<CreateUserResponse> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ReadUserResponse> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<ReadUserResponse> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}