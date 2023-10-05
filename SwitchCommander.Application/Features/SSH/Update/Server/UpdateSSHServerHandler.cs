using MediatR;
using Microsoft.Extensions.Logging;
using SwitchCommander.Application.Common.Services;
using SwitchCommander.Application.Repositories.Features.SSH;

namespace SwitchCommander.Application.Features.SSH.Update.Server;

public sealed record UpdateSSHServerRequest(string Id, string Hostname, string Username, string Password) : IRequest<UpdateSSHServerResponse>;

public sealed record UpdateSSHServerResponse(bool result);


public class UpdateSSHServerHandler : IRequestHandler<UpdateSSHServerRequest, UpdateSSHServerResponse>
{
    private readonly ILogger<UpdateSSHServerHandler> _logger;
    private readonly ISSHServerRepository _repository;
    private readonly UpdateSSHServerMapper _mapper;
    private readonly IPasswordService _passwordService;
    
    public UpdateSSHServerHandler(ILogger<UpdateSSHServerHandler> logger, ISSHServerRepository repository, UpdateSSHServerMapper mapper, IPasswordService passwordService)
    {
        _logger = logger;
        _repository = repository;
        _mapper = mapper;
        _passwordService = passwordService;
    }

    public async Task<UpdateSSHServerResponse> Handle(UpdateSSHServerRequest request, CancellationToken cancellationToken)
    {
        var mapper = _mapper.FromRequest(request);
        mapper.Password = await _passwordService.HashPassword(request.Password);
        var result = await _repository.UpdateAsync(mapper, cancellationToken);
        return new UpdateSSHServerResponse(result);
    }
}