using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SwitchCommander.Application.Repositories.Features.SSH;
using SwitchCommander.Domain.Dtos.SSH;

namespace SwitchCommander.Application.Features.Import;

public sealed record CreateSSHImportJsonRequest(string Json) : IRequest<CreateSSHImportJsonResponse>;

public sealed record CreateSSHImportJsonResponse(bool isImported);

sealed record CreateSSHImportJsonBlob(
    IEnumerable<SSHCommand> SshCommands,
    IEnumerable<SSHServer> SshServers,
    IEnumerable<SSHSequence> SshSequences,
    IEnumerable<SShCommandConfiguration> SShCommandConfigurations
);

public class CreateSSHImportJsonHandler : IRequestHandler<CreateSSHImportJsonRequest, CreateSSHImportJsonResponse>
{
    private readonly ILogger<CreateSSHImportJsonHandler> _logger;
    private readonly ISshCommandMongoRepository _commandMongoRepository;
    private readonly ISshSequenceMongoRepository _sequenceMongoRepository;
    private readonly ISshServerMongoRepository _sshServerMongoRepository;
    private readonly ISshCommandConfigurationMongoRepository _configurationMongoRepository;

    public CreateSSHImportJsonHandler(
        ILogger<CreateSSHImportJsonHandler> logger,
        ISshCommandMongoRepository commandMongoRepository,
        ISshSequenceMongoRepository sequenceMongoRepository,
        ISshServerMongoRepository sshServerMongoRepository,
        ISshCommandConfigurationMongoRepository configurationMongoRepository)
    {
        _logger = logger;
        _commandMongoRepository = commandMongoRepository;
        _sequenceMongoRepository = sequenceMongoRepository;
        _sshServerMongoRepository = sshServerMongoRepository;
        _configurationMongoRepository = configurationMongoRepository;
    }

    /// <summary>
    /// Create the imports object as new elements / update: <seealso cref="UpdateSSHImportJsonHandler"/>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<CreateSSHImportJsonResponse> Handle(CreateSSHImportJsonRequest request,
        CancellationToken cancellationToken)
    {
        var jsonObject = JsonConvert.DeserializeObject<CreateSSHImportJsonBlob>(request.Json);

        foreach (var jsonObjectSshCommand in jsonObject.SshCommands)
        {
            await _commandMongoRepository.AddAsync(jsonObjectSshCommand, cancellationToken);
            _logger.LogInformation("SshCommands were imported " + jsonObjectSshCommand.Id);
        }

        foreach (var jsonObjectSshSequence in jsonObject.SshSequences)
        {
            await _sequenceMongoRepository.AddAsync(jsonObjectSshSequence, cancellationToken);
            _logger.LogInformation("SshSequences were imported " + jsonObjectSshSequence.Id);
        }

        foreach (var jsonObjectSshServer in jsonObject.SshServers)
        {
            await _sshServerMongoRepository.AddAsync(jsonObjectSshServer, cancellationToken);
            _logger.LogInformation("SshServers were imported " + jsonObjectSshServer.Id);
        }

        foreach (var jsonObjectSShCommandConfiguration in jsonObject.SShCommandConfigurations)
        {
            await _configurationMongoRepository.AddAsync(jsonObjectSShCommandConfiguration, cancellationToken);
            _logger.LogInformation("SShCommandConfigurations were imported " + jsonObjectSShCommandConfiguration.Id);
        }

        return new CreateSSHImportJsonResponse(true);
    }
}