using MediatR;
using Microsoft.Extensions.Logging;
using SwitchCommander.Application.Repositories.Features.SSH;

namespace SwitchCommander.Application.Features.SSH.Create.Command;

public sealed record CreateSSHCommandRequest
    (string? Name, string? Description, string? Command) : IRequest<CreateSSHCommandResponse>;

public sealed record CreateSSHCommandResponse(Guid? Id, string? Name, string? Description, string? Command);

public class CreateSSHCommandHandler : IRequestHandler<CreateSSHCommandRequest, CreateSSHCommandResponse>
{
    private readonly ILogger<CreateSSHCommandHandler> _logger;
    private readonly CreateSSHCommandMapper _mapper;
    private readonly ISshCommandMongoRepository _mongoRepository;

    public CreateSSHCommandHandler(ILogger<CreateSSHCommandHandler> logger, CreateSSHCommandMapper mapper,
        ISshCommandMongoRepository mongoRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _mongoRepository = mongoRepository;
    }

    public async Task<CreateSSHCommandResponse> Handle(CreateSSHCommandRequest request,
        CancellationToken cancellationToken)
    {
        var map = _mapper.FromRequest(request);
        await _mongoRepository.AddAsync(map, cancellationToken);
        return _mapper.ToResponse(map);
    }
}