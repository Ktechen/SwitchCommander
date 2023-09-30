using MediatR;
using Microsoft.Extensions.Logging;
using SwitchCommander.Application.Repositories.Features;

namespace SwitchCommander.Application.Features.SSH.CreateSSHCommand;

public sealed record CreateSSHCommandRequest
    (string? Name, string? Description, string? Command) : IRequest<CreateSSHCommandResponse>;

public sealed record CreateSSHCommandResponse(string? Name, string? Description, string? Command);

public class CreateSSHCommandHandler : IRequestHandler<CreateSSHCommandRequest, CreateSSHCommandResponse>
{
    private readonly ILogger<CreateSSHCommandHandler> _logger;
    private readonly CreateSSHCommandMapper _mapper;
    private readonly ISSHCommandRepository _repository;

    public CreateSSHCommandHandler(ILogger<CreateSSHCommandHandler> logger, CreateSSHCommandMapper mapper,
        ISSHCommandRepository repository)
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