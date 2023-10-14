using MediatR;
using Microsoft.Extensions.Logging;
using SwitchCommander.Application.Repositories.Features.SSH;

namespace SwitchCommander.Application.Features.SSH.Read.Config;

public sealed record ReadAllSSHCommandConfigurationRequest
    () : IRequest<IEnumerable<ReadAllSSHCommandConfigurationResponse>>;

public sealed record ReadAllSSHCommandConfigurationResponse(Guid? Id, string? Hostname, string? Username,
    long DateCreated,
    long? DateUpdated);

public class ReadAllSSHConfigurationHandler : IRequestHandler<ReadAllSSHCommandConfigurationRequest,
    IEnumerable<ReadAllSSHCommandConfigurationResponse>>
{
    private readonly ILogger<ReadAllSSHConfigurationHandler> _logger;
    private readonly ReadSSHConfigurationMapper _mapper;
    private readonly ISshCommandConfigurationMongoRepository _mongoRepository;

    public ReadAllSSHConfigurationHandler(ILogger<ReadAllSSHConfigurationHandler> logger,
        ISshCommandConfigurationMongoRepository mongoRepository,
        ReadSSHConfigurationMapper mapper)
    {
        _logger = logger;
        _mongoRepository = mongoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ReadAllSSHCommandConfigurationResponse>> Handle(ReadAllSSHCommandConfigurationRequest request, CancellationToken cancellationToken)
    {
        var result = await _mongoRepository.ReadAllAsync(cancellationToken);
        var mapping = _mapper.ToAllResponse(result);
        return mapping;
    }
}