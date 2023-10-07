using MediatR;
using Microsoft.Extensions.Logging;
using SwitchCommander.Application.Repositories.Features.SSH;

namespace SwitchCommander.Application.Features.SSH.Update.Command;

public sealed record UpdateSSHCommandRequest(string Id, string Name, string Description, string Command) : IRequest<UpdateSSHCommandResponse>;

public sealed record UpdateSSHCommandResponse(bool result);


public class UpdateSSHCommandHandler : IRequestHandler<UpdateSSHCommandRequest, UpdateSSHCommandResponse>
{
    private readonly ILogger<UpdateSSHCommandHandler> _logger;
    private readonly UpdateSSHCommandMapper _mapper;
    private readonly ISSHCommandRepository _repository;

    public UpdateSSHCommandHandler(
        ILogger<UpdateSSHCommandHandler> logger, 
        UpdateSSHCommandMapper mapper, 
        ISSHCommandRepository repository)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<UpdateSSHCommandResponse> Handle(UpdateSSHCommandRequest request, CancellationToken cancellationToken)
    {
        var mapper = _mapper.FromRequest(request);
        var result = await _repository.ReplaceAsync(mapper, cancellationToken);
        return new UpdateSSHCommandResponse(result);
    }
}