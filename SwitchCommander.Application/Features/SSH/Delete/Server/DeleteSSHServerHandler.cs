using MediatR;
using Microsoft.Extensions.Logging;
using SwitchCommander.Application.Repositories.Features.SSH;

namespace SwitchCommander.Application.Features.SSH.Delete.Server;

public sealed record DeleteSSHServerRequest(Guid Id) : IRequest<DeleteSSHServerResponse>;

public sealed record DeleteSSHServerResponse(bool Deleted);

public class DeleteSSHServerHandler : IRequestHandler<DeleteSSHServerRequest, DeleteSSHServerResponse>
{
    private readonly ILogger<DeleteSSHServerHandler> _logger;
    private readonly ISSHServerRepository _repository;
    
    public DeleteSSHServerHandler(ILogger<DeleteSSHServerHandler> logger, ISSHServerRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<DeleteSSHServerResponse> Handle(DeleteSSHServerRequest request, CancellationToken cancellationToken)
    {
        var result = await _repository.DeleteAsync(request.Id, cancellationToken);
        _logger.LogInformation("Delete by Id: {0} result: {1}", request.Id, result);
        return new DeleteSSHServerResponse(result);
    }
}