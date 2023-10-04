using MediatR;
using Microsoft.Extensions.Logging;
using Renci.SshNet;
using SwitchCommander.Application.Repositories.Features.SSH;

namespace SwitchCommander.Application.Features.SSH;

public sealed record ExecuteSSHCommandHandlerRequest
    (Guid serverId, Guid commandId) : IRequest<ExecuteSSHCommandHandlerResponse>;

public sealed record ExecuteSSHCommandHandlerResponse(string? Hostname, string? Username, string? Name,
    string? Description, string? Command, string? CommandResult);

public class ExecuteSSHCommandHandler : IRequestHandler<ExecuteSSHCommandHandlerRequest, ExecuteSSHCommandHandlerResponse>
{
    private readonly ILogger<ExecuteSSHCommandHandler> _logger;
    private readonly ISSHServerRepository _serverRepository;
    private readonly ISSHCommandRepository _commandRepository;

    public ExecuteSSHCommandHandler(ILogger<ExecuteSSHCommandHandler> logger, ISSHServerRepository serverRepository,
        ISSHCommandRepository commandRepository)
    {
        _logger = logger;
        _serverRepository = serverRepository;
        _commandRepository = commandRepository;
    }

    public async Task<ExecuteSSHCommandHandlerResponse> Handle(ExecuteSSHCommandHandlerRequest request,
        CancellationToken cancellationToken)
    {
        var server = await _serverRepository.GetByIdAsync(request.serverId, cancellationToken);
        var sshCommand = await _commandRepository.GetByIdAsync(request.commandId, cancellationToken);

        if (server is null || sshCommand is null)
        {
            _logger.LogError("server is null || sshCommand is null");
            return new ExecuteSSHCommandHandlerResponse(null, null, null, null, null,
                "server is null or sshCommand is null");
        }

        using var client = new SshClient(server.Hostname, server.Username, server.Password);
        _logger.LogInformation("client connected: " + client.IsConnected);
        var runCommand = client.RunCommand(sshCommand.Command);
        client.Disconnect();

        _logger.LogInformation("command result: " + runCommand.Result);
        _logger.LogInformation("client ExitStatus: " + runCommand.ExitStatus);

        return runCommand.Error.Length != 0
            ? new ExecuteSSHCommandHandlerResponse(server.Hostname, server.Username, sshCommand.Name,
                sshCommand.Description, sshCommand.Command, runCommand.Error)
            : new ExecuteSSHCommandHandlerResponse(server.Hostname, server.Username, sshCommand.Name,
                sshCommand.Description, sshCommand.Command, runCommand.Result);
    }
}