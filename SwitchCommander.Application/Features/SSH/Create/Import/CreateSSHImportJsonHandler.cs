using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SwitchCommander.Application.Features.SSH.Create.Command;
using SwitchCommander.Application.Features.SSH.Create.Sequence;
using SwitchCommander.Application.Features.SSH.Create.Server;
using SwitchCommander.Application.Features.SSH.Update.Config;
using SwitchCommander.Application.Features.SSH.Update.Import;
using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Features.SSH.Create.Import;

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
    private readonly IMediator _mediator;

    public CreateSSHImportJsonHandler(
        ILogger<CreateSSHImportJsonHandler> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
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
            await _mediator.Send(
                new CreateSSHCommandRequest(
                    jsonObjectSshCommand.Name,
                    jsonObjectSshCommand.Description,
                    jsonObjectSshCommand.Command
                ), cancellationToken
            );
        }

        foreach (var jsonObjectSshSequence in jsonObject.SshSequences)
        {
            await _mediator.Send(
                new CreateSSHCommandSequenceRequest(
                    jsonObjectSshSequence.SequenceName,
                    jsonObjectSshSequence.SshCommands
                ), cancellationToken);
        }

        foreach (var jsonObjectSshServer in jsonObject.SshServers)
        {
            await _mediator.Send(
                new CreateSSHServerRequest(
                    jsonObjectSshServer.Hostname,
                    jsonObjectSshServer.Username,
                    jsonObjectSshServer.Password
                ), cancellationToken);
        }

        foreach (var jsonObjectSShCommandConfiguration in jsonObject.SShCommandConfigurations)
        {
            await _mediator.Send(
                new UpdateSSHCommandConfigurationRequest(
                    jsonObjectSShCommandConfiguration.Id,
                    jsonObjectSShCommandConfiguration.CommandMinimumLength,
                    jsonObjectSShCommandConfiguration.CommandMaximumLength,
                    jsonObjectSShCommandConfiguration.DescriptionMinimumLength,
                    jsonObjectSShCommandConfiguration.DescriptionMaximumLength
                ), cancellationToken);
        }

        return new CreateSSHImportJsonResponse(true);
    }
}