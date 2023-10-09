using MediatR;
using Microsoft.Extensions.Logging;
using SwitchCommander.Application.Common.Services;
using SwitchCommander.Application.Repositories.Features;

namespace SwitchCommander.Application.Features.User;

public sealed record LoginRequest(string Email, string Password) : IRequest<LoginResponse>;

public sealed record LoginResponse(bool IsLogged);

public class LoginHandler : IRequestHandler<LoginRequest, LoginResponse>
{
    private readonly ILogger<LoginHandler> _logger;
    private readonly IPasswordService _passwordService;
    private readonly IUserMongoRepository _userMongoRepository;

    public LoginHandler(ILogger<LoginHandler> logger, IPasswordService passwordService,
        IUserMongoRepository userMongoRepository)
    {
        _logger = logger;
        _passwordService = passwordService;
        _userMongoRepository = userMongoRepository;
    }

    public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var resultEmailFound = await _userMongoRepository.GetByEmail(request.Email, cancellationToken);
        if (resultEmailFound is null)
        {
            _logger.LogError("Email {0} not found", request.Email);
            return new LoginResponse(false);
        }

        var resultPasswordValidation =
            await _passwordService.VerifyPassword(request.Password, resultEmailFound.Password);
        if (resultPasswordValidation) return new LoginResponse(true);

        _logger.LogError("Password is invalid | Email: {0}", request.Email);
        return new LoginResponse(false);
    }
}