using MediatR;
using Microsoft.Extensions.Logging;

namespace SwitchCommander.Application.Features.SSH.CreateSSHCommand;

public sealed record CreateSSHServerRequest
    (string? Hostname, string? Username, string? Password) : IRequest<CreateSSHServerResponse>;

public sealed record CreateSSHServerResponse(string? Hostname, string? Username);

public class CreateSSHServerHandler : IRequestHandler<CreateSSHServerRequest, CreateSSHServerResponse>
{
    private readonly ILogger<CreateSSHServerHandler> _logger;

    public CreateSSHServerHandler(ILogger<CreateSSHServerHandler> logger)
    {
        _logger = logger;
    }

    public Task<CreateSSHServerResponse> Handle(CreateSSHServerRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Create a server {0} ", request.Hostname);
        return null;
    }
}