using MediatR;
using Microsoft.Extensions.Logging;
using SwitchCommander.Application.Common.Services;
using SwitchCommander.Application.Repositories.Features.SSH;

namespace SwitchCommander.Application.Features.SSH.Create.Server;

public sealed record CreateSSHServerRequest
    (string? Hostname, string? Username, string? Password) : IRequest<CreateSSHServerResponse>;

public sealed record CreateSSHServerResponse(string? Hostname, string? Username);

public class CreateSSHServerHandler : IRequestHandler<CreateSSHServerRequest, CreateSSHServerResponse>
{
    private readonly ILogger<CreateSSHServerHandler> _logger;
    private readonly CreateSSHServerMapper _mapper;
    private readonly ISshServerMongoRepository _mongoRepository;
    private readonly IPasswordService _passwordService;

    public CreateSSHServerHandler(ILogger<CreateSSHServerHandler> logger,
        ISshServerMongoRepository serverMongoRepository, CreateSSHServerMapper mapper, IPasswordService passwordService,
        IPingService pingService)
    {
        _logger = logger;
        _mongoRepository = serverMongoRepository;
        _mapper = mapper;
        _passwordService = passwordService;
    }

    public async Task<CreateSSHServerResponse> Handle(CreateSSHServerRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Create a server {0} ", request.Hostname);
        var map = _mapper.FromRequest(request);
        map.Password = await _passwordService.HashPassword(map.Password);
        await _mongoRepository.AddAsync(map, cancellationToken);
        
        return _mapper.ToResponse(map);
    }
}