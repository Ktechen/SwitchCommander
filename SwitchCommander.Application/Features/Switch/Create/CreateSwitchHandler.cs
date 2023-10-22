using MediatR;

namespace SwitchCommander.Application.Features.Switch.Create;

public sealed record CreateSwitchRequest
    (string? Name, string? Description, string? Command) : IRequest<CreateSwitchResponse>;

public sealed record CreateSwitchResponse(Guid? Id, string? Name, string? Description, string? Command);


public class CreateSwitchHandler : IRequestHandler<CreateSwitchRequest, CreateSwitchResponse>
{
    public Task<CreateSwitchResponse> Handle(CreateSwitchRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}