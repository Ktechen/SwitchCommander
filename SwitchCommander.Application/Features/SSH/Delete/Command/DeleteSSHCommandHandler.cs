using MediatR;
using Microsoft.Extensions.Logging;
using SwitchCommander.Application.Repositories.Features.SSH;

namespace SwitchCommander.Application.Features.SSH.Delete.Command;

public sealed record DeleteSSHCommandRequest(Guid Id) : IRequest<DeleteSSHCommandResponse>;

public sealed record DeleteSSHCommandResponse(bool Deleted);

public class DeleteSSHCommandHandler : IRequestHandler<DeleteSSHCommandRequest, DeleteSSHCommandResponse>
{
    private readonly ILogger<DeleteSSHCommandHandler> _logger;
    private readonly ISSHCommandRepository _repository;

    public DeleteSSHCommandHandler(ILogger<DeleteSSHCommandHandler> logger, ISSHCommandRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<DeleteSSHCommandResponse> Handle(DeleteSSHCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _repository.DeleteAsync(request.Id, cancellationToken);
        _logger.LogInformation("Delete by Id: {0} result: {1}", request.Id, result);
        return new DeleteSSHCommandResponse(result);
    }
}