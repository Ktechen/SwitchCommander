using MediatR;
using Newtonsoft.Json;
using SwitchCommander.Application.Features.SSH.Update.Command;
using SwitchCommander.Application.Features.SSH.Update.Config;
using SwitchCommander.Application.Features.SSH.Update.Server;
using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Features.SSH.Update.Import;

public sealed record UpdateSSHImportJsonRequest(string Json) : IRequest;

sealed record UpdateSSHImportJsonBlob(
    IEnumerable<SSHCommand> SshCommands,
    IEnumerable<SSHServer> SshServers,
    IEnumerable<SSHSequence> SshSequences,
    IEnumerable<SShCommandConfiguration> SShCommandConfigurations
);

public class UpdateSSHImportJsonHandler : IRequestHandler<UpdateSSHImportJsonRequest>
{
    private readonly IMediator _mediator;

    public UpdateSSHImportJsonHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(UpdateSSHImportJsonRequest request, CancellationToken cancellationToken)
    {
        var jsonObject = JsonConvert.DeserializeObject<UpdateSSHImportJsonBlob>(request.Json);

        foreach (var jsonObjectSshCommand in jsonObject.SshCommands)
        {
            await _mediator.Send(new UpdateSSHCommandRequest(
                    jsonObjectSshCommand.Id,
                    jsonObjectSshCommand.Name,
                    jsonObjectSshCommand.Description,
                    jsonObjectSshCommand.Command
                ), cancellationToken
            );
        }

        foreach (var jsonObjectSshSequence in jsonObject.SshSequences)
        {
            //Todo new to be impl. ... see you kevin in the future
        }

        foreach (var jsonObjectSshServer in jsonObject.SshServers)
        {
            await _mediator.Send(
                new UpdateSSHServerRequest(
                    jsonObjectSshServer.Id,
                    jsonObjectSshServer.Hostname,
                    jsonObjectSshServer.Username)
                , cancellationToken
            );
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
    }
}