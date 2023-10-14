namespace SwitchCommander.Blazor.Services;

public class UserService : IUserClient
{
    private IUserClient _userClientImplementation;

    public UserService(IUserClient userClientImplementation)
    {
        _userClientImplementation = userClientImplementation;
    }

    public Task<CreateUserResponse> CreateAsync(CreateUserRequest request)
    {
        return _userClientImplementation.CreateAsync(request);
    }

    public Task<CreateUserResponse> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        return _userClientImplementation.CreateAsync(request, cancellationToken);
    }

    public Task<ReadUserResponse> GetByIdAsync(string id)
    {
        return _userClientImplementation.GetByIdAsync(id);
    }

    public Task<ReadUserResponse> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        return _userClientImplementation.GetByIdAsync(id, cancellationToken);
    }
}