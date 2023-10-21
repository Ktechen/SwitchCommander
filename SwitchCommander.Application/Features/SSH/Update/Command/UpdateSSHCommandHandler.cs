using MediatR;
using Microsoft.Extensions.Logging;
using SwitchCommander.Application.Repositories.Features.SSH;

namespace SwitchCommander.Application.Features.SSH.Update.Command;

public sealed record UpdateSSHCommandRequest
    (Guid Id, string? Name, string? Description, string? Command) : IRequest<UpdateSSHCommandResponse>;

public sealed record UpdateSSHCommandResponse(bool result);

public class UpdateSSHCommandHandler : IRequestHandler<UpdateSSHCommandRequest, UpdateSSHCommandResponse>
{
    private readonly ILogger<UpdateSSHCommandHandler> _logger;
    private readonly UpdateSSHCommandMapper _mapper;
    private readonly ISshCommandMongoRepository _mongoRepository;

    public UpdateSSHCommandHandler(
        ILogger<UpdateSSHCommandHandler> logger,
        UpdateSSHCommandMapper mapper,
        ISshCommandMongoRepository mongoRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _mongoRepository = mongoRepository;
    }

    public async Task<UpdateSSHCommandResponse> Handle(UpdateSSHCommandRequest request,
        CancellationToken cancellationToken)
    {
        var mapper = _mapper.FromRequest(request);
        var result = await _mongoRepository.ReplaceAsync(mapper, cancellationToken);
        return new UpdateSSHCommandResponse(result);
    }
}