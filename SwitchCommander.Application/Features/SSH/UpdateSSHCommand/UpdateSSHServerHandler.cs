using MediatR;

namespace SwitchCommander.Application.Features.SSH.UpdateSSHCommand;

public sealed record UpdateSSHServerRequest(Guid Id) : IRequest<UpdateSSHServerResponse>;

public sealed record UpdateSSHServerResponse(bool result);


public class UpdateSSHServerHandler
{
    
}