using MediatR;

namespace SwitchCommander.Application.Features.SSH.UpdateSSHCommand;

public sealed record UpdateSSHServerRequest(Guid Id, string Hostname, string Username, string Password) : IRequest<UpdateSSHServerResponse>;

public sealed record UpdateSSHServerResponse(bool result);


public class UpdateSSHServerHandler
{
    
}