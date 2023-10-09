using MediatR;
using Microsoft.Extensions.Logging;
using SwitchCommander.Application.Repositories.Features.SSH;

namespace SwitchCommander.Application.Features.SSH.Delete.Server;

public sealed record DeleteSSHServerRequest(Guid Id) : IRequest<DeleteSSHServerResponse>;

public sealed record DeleteSSHServerResponse(bool Deleted);

public class DeleteSSHServerHandler : IRequestHandler<DeleteSSHServerRequest, DeleteSSHServerResponse>
{
    private readonly ILogger<DeleteSSHServerHandler> _logger;
    private readonly ISshServerMongoRepository _mongoRepository;

    public DeleteSSHServerHandler(ILogger<DeleteSSHServerHandler> logger, ISshServerMongoRepository mongoRepository)
    {
        _logger = logger;
        _mongoRepository = mongoRepository;
    }

    public async Task<DeleteSSHServerResponse> Handle(DeleteSSHServerRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _mongoRepository.DeleteAsync(request.Id, cancellationToken);
        _logger.LogInformation("Delete by Id: {0} result: {1}", request.Id, result);
        return new DeleteSSHServerResponse(result);
    }
}