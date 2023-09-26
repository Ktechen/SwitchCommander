using MediatR;
using Microsoft.Extensions.Logging;
using SwitchCommander.Application.Repositories.Features;

namespace SwitchCommander.Application.Features.SSHManagement.CreateSSHCommand;

public sealed record CreateSSHCommandRequest
    (Guid id, string? Hash, string? command) : IRequest<CreateSSHCommandResponse>;

public sealed record CreateSSHCommandResponse(Guid? id, string? Hash, string? command);

public class CreateSSHCommandHandler : IRequestHandler<CreateSSHCommandRequest, CreateSSHCommandResponse>
{
    private readonly ILogger<CreateSSHCommandHandler> _logger;
    private readonly CreateSSHCommandMapper _mapper;
    private readonly ISSHCommanderRepository _repository;

    public CreateSSHCommandHandler(ILogger<CreateSSHCommandHandler> logger, CreateSSHCommandMapper mapper,
        ISSHCommanderRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<CreateSSHCommandResponse> Handle(CreateSSHCommandRequest request,
        CancellationToken cancellationToken)
    {
        var map = _mapper.FromRequest(request);
        await _repository.AddAsync(map, cancellationToken);

        return _mapper.ToResponse(map);
    }
}