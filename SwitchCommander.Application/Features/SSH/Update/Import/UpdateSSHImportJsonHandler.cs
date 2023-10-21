using MediatR;
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
    public Task Handle(UpdateSSHImportJsonRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}