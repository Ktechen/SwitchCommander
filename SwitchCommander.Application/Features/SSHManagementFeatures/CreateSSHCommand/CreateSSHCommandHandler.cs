using MediatR;

namespace SwitchCommander.Application.Features.SSHManagementFeatures.CreateSSHCommand;

public sealed record CreateSSHCommandRequest
    (Guid id, string? Hash, string? command) : IRequest<CreateSSHCommandResponse>;

public sealed record CreateSSHCommandResponse(Guid id, string? Hash, string? command);

public class CreateSSHCommandHandler : IRequestHandler<CreateSSHCommandRequest, CreateSSHCommandResponse>
{
    public Task<CreateSSHCommandResponse> Handle(CreateSSHCommandRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}