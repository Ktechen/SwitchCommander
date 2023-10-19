using MediatR;
using Microsoft.Extensions.Logging;
using SwitchCommander.Application.Repositories.Features.SSH;

namespace SwitchCommander.Application.Features.SSH.Read.Server;

public sealed record ReadAllSSHServerRequest : IRequest<IEnumerable<ReadAllSSHServerResponse>>;

public sealed record ReadAllSSHServerResponse(Guid? Id, string? Hostname, string? Username, long DateCreated,
    long? DateUpdated);

public class ReadAllSSHServerHandler : IRequestHandler<ReadAllSSHServerRequest, IEnumerable<ReadAllSSHServerResponse>>
{
    private readonly ILogger<ReadAllSSHServerHandler> _logger;
    private readonly ReadSSHServerMapper _mapper;
    private readonly ISshServerMongoRepository _mongoRepository;

    public ReadAllSSHServerHandler(ILogger<ReadAllSSHServerHandler> logger, ISshServerMongoRepository mongoRepository,
        ReadSSHServerMapper mapper)
    {
        _logger = logger;
        _mongoRepository = mongoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ReadAllSSHServerResponse>> Handle(ReadAllSSHServerRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _mongoRepository.ReadAllAsync(cancellationToken);
        var mapping = _mapper.ToResponse(result);
        return mapping;
    }
}