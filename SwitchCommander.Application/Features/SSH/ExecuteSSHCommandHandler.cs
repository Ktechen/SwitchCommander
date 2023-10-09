using MediatR;
using Microsoft.Extensions.Logging;
using Renci.SshNet;
using SwitchCommander.Application.Common.Exceptions;
using SwitchCommander.Application.Common.Services;
using SwitchCommander.Application.Repositories.Features.SSH;

namespace SwitchCommander.Application.Features.SSH;

public sealed record ExecuteSSHCommandRequest
    (string ServerId, string CommandId, string Password) : IRequest<ExecuteSSHCommandResponse>;

public sealed record ExecuteSSHCommandResponse(string? Hostname, string? Username, string? Name,
    string? Description, string? Command, string? CommandResult);

public class ExecuteSSHCommandHandler : IRequestHandler<ExecuteSSHCommandRequest, ExecuteSSHCommandResponse>
{
    private readonly ISshCommandMongoRepository _commandMongoRepository;
    private readonly ILogger<ExecuteSSHCommandHandler> _logger;
    private readonly IPasswordService _passwordService;
    private readonly ISshServerMongoRepository _serverMongoRepository;

    public ExecuteSSHCommandHandler(
        ILogger<ExecuteSSHCommandHandler> logger,
        ISshServerMongoRepository serverMongoRepository,
        ISshCommandMongoRepository commandMongoRepository,
        IPasswordService passwordService)
    {
        _logger = logger;
        _serverMongoRepository = serverMongoRepository;
        _commandMongoRepository = commandMongoRepository;
        _passwordService = passwordService;
    }

    public async Task<ExecuteSSHCommandResponse> Handle(ExecuteSSHCommandRequest request,
        CancellationToken cancellationToken)
    {
        var server = await _serverMongoRepository.FindByIdAsync(new Guid(request.ServerId), cancellationToken);
        var sshCommand = await _commandMongoRepository.FindByIdAsync(new Guid(request.CommandId), cancellationToken);

        if (server is null || sshCommand is null)
        {
            _logger.LogError("server is null || sshCommand is null");
            throw new BadRequestException("server is null or sshCommand is null");
        }

        var password = await _passwordService.VerifyPassword(request.Password, server.Password);
        if (!password)
        {
            _logger.LogError("password is invalid");
            throw new BadRequestException("password is invalid");
        }

        try
        {
            using var client = new SshClient(server.Hostname, server.Username, server.Password);
            _logger.LogInformation("client connected: " + client.IsConnected);
            var runCommand = client.RunCommand(sshCommand.Command);
            client.Disconnect();

            _logger.LogInformation("command result: " + runCommand.Result);
            _logger.LogInformation("client ExitStatus: " + runCommand.ExitStatus);
            return runCommand.Error.Length != 0
                ? new ExecuteSSHCommandResponse(server.Hostname, server.Username, sshCommand.Name,
                    sshCommand.Description, sshCommand.Command, runCommand.Error)
                : new ExecuteSSHCommandResponse(server.Hostname, server.Username, sshCommand.Name,
                    sshCommand.Description, sshCommand.Command, runCommand.Result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw new SSHNetException(e.Message);
        }
    }
}