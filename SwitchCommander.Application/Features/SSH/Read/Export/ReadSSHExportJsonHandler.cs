using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SwitchCommander.Application.Common.Enums;
using SwitchCommander.Application.Common.Exceptions;
using SwitchCommander.Application.Repositories.Features.SSH;
using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Features.SSH.Read.Export;

/// <summary>
/// Request for json request
/// </summary>
/// <param name="Type">command, server, sequence, config</param>
public sealed record ReadSSHExportJsonRequest(SshImportExportType Type) : IRequest<ReadSSHExportJsonResponse>;

public sealed record ReadSSHExportJsonResponse(string Json);

sealed record ReadSSHExportJsonBlob(
    IEnumerable<SSHCommand> SshCommands,
    IEnumerable<SSHServer> SshServers,
    IEnumerable<SSHSequence> SshSequences,
    IEnumerable<SShCommandConfiguration> SShCommandConfigurations
);

public class ReadSSHExportJsonHandler : IRequestHandler<ReadSSHExportJsonRequest, ReadSSHExportJsonResponse>
{
    private readonly ILogger<ReadSSHExportJsonHandler> _logger;
    private readonly ISshCommandMongoRepository _commandMongoRepository;
    private readonly ISshServerMongoRepository _sshServerMongoRepository;
    private readonly ISshSequenceMongoRepository _sequenceMongoRepository;
    private readonly ISshCommandConfigurationMongoRepository _configurationMongoRepository;

    public ReadSSHExportJsonHandler(
        ILogger<ReadSSHExportJsonHandler> logger,
        ISshCommandMongoRepository commandMongoRepository, ISshServerMongoRepository sshServerMongoRepository,
        ISshSequenceMongoRepository sequenceMongoRepository,
        ISshCommandConfigurationMongoRepository configurationMongoRepository)
    {
        _logger = logger;
        _commandMongoRepository = commandMongoRepository;
        _sshServerMongoRepository = sshServerMongoRepository;
        _sequenceMongoRepository = sequenceMongoRepository;
        _configurationMongoRepository = configurationMongoRepository;
    }

    public async Task<ReadSSHExportJsonResponse> Handle(ReadSSHExportJsonRequest request,
        CancellationToken cancellationToken)
    {
        switch (request.Type)
        {
            case SshImportExportType.Command:
                var commands = await _commandMongoRepository.ReadAllAsync(cancellationToken);
                _logger.LogInformation("Export count of commands " + commands.Count());
                var jsonCommand = JsonConvert.SerializeObject(commands);
                return new ReadSSHExportJsonResponse(jsonCommand);

            case SshImportExportType.Server:
                var servers = await _sshServerMongoRepository.ReadAllAsync(cancellationToken);
                _logger.LogInformation("Export count of server " + servers.Count());
                var jsonServer = JsonConvert.SerializeObject(servers);
                return new ReadSSHExportJsonResponse(jsonServer);

            case SshImportExportType.Sequence:
                var sequences = await _sequenceMongoRepository.ReadAllAsync(cancellationToken);
                _logger.LogInformation("Export count of sequence " + sequences.Count());
                var jsonSequence = JsonConvert.SerializeObject(sequences);
                return new ReadSSHExportJsonResponse(jsonSequence);

            case SshImportExportType.Config:
                var configs = await _configurationMongoRepository.ReadAllAsync(cancellationToken);
                _logger.LogInformation("Export count of config " + configs.Count());
                var jsonConfig = JsonConvert.SerializeObject(configs);
                return new ReadSSHExportJsonResponse(jsonConfig);

            case SshImportExportType.Blob:
                var commandsBlob = await _commandMongoRepository.ReadAllAsync(cancellationToken);
                var serverBlob = await _sshServerMongoRepository.ReadAllAsync(cancellationToken);
                var sequenceBlob = await _sequenceMongoRepository.ReadAllAsync(cancellationToken);
                var configBlob = await _configurationMongoRepository.ReadAllAsync(cancellationToken);
                var blob = new ReadSSHExportJsonBlob(commandsBlob, serverBlob, sequenceBlob, configBlob);

                _logger.LogWarning("Blog Export of all elements");
                var jsonBlob = JsonConvert.SerializeObject(blob);
                return new ReadSSHExportJsonResponse(jsonBlob);

            default:
                throw new ExportTypeException("Unknown export type selected");
        }
    }
}