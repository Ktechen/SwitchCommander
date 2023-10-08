using MediatR;
using Microsoft.Extensions.Logging;
using SwitchCommander.Application.Repositories.Features.SSH;

namespace SwitchCommander.Application.Features.SSH.Read.Server;

public sealed record ReadSSHServerRequest(Guid Id) : IRequest<ReadSSHServerResponse>;

public sealed record ReadSSHServerResponse(Guid? Id, string? Hostname, string? Username, long DateCreated, long? DateUpdated );

public class ReadSSHServerHandler : IRequestHandler<ReadSSHServerRequest, ReadSSHServerResponse>
{
    private readonly ILogger<ReadSSHServerHandler> _logger;
    private readonly ISshServerMongoRepository _mongoRepository;
    private readonly ReadSSHServerMapper _mapper;

    public ReadSSHServerHandler(ILogger<ReadSSHServerHandler> logger, ISshServerMongoRepository mongoRepository, ReadSSHServerMapper mapper)
    {
        _logger = logger;
        _mongoRepository = mongoRepository;
        _mapper = mapper;
    }

    public async Task<ReadSSHServerResponse> Handle(ReadSSHServerRequest request, CancellationToken cancellationToken)
    {
        var result = await _mongoRepository.FindByIdAsync(request.Id, cancellationToken);
        if (result is null)
        {
            return new ReadSSHServerResponse(null, null, null, 0, 0);
        }
        
        _logger.LogInformation("Read by Id: {0} result: {1}", request.Id, result);
        var map = _mapper.ToResponse(result);
        return map;
    }
}