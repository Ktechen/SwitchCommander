using MediatR;

namespace SwitchCommander.Application.Features.SSH.Update;

public sealed record UpdateSSHCommandRequest(Guid Id) : IRequest<UpdateSSHCommandResponse>;

public sealed record UpdateSSHCommandResponse(bool result);


public class UpdateSSHCommandHandler
{
    
}