using MediatR;
using SwitchCommander.Application.Repositories.Features.SSH;

namespace SwitchCommander.Application.Features.SSH.Read.Config;

public sealed record ReadSSHCommandConfigurationRequest : IRequest<IEnumerable<ReadSSHCommandConfigurationResponse>>;

public sealed record ReadSSHCommandConfigurationResponse(
    int CommandMinimumLength, int CommandMaximumLength, int DescriptionMinimumLength, int DescriptionMaximumLength
);

public class ReadSSHConfigurationHandler : IRequestHandler<ReadSSHCommandConfigurationRequest,
    IEnumerable<ReadSSHCommandConfigurationResponse>>
{
    private readonly ReadSSHConfigurationMapper _mapper;
    private readonly ISshCommandConfigurationMongoRepository _mongoRepository;

    public ReadSSHConfigurationHandler(ISshCommandConfigurationMongoRepository mongoRepository,
        ReadSSHConfigurationMapper mapper)
    {
        _mongoRepository = mongoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ReadSSHCommandConfigurationResponse>> Handle(
        ReadSSHCommandConfigurationRequest request, CancellationToken cancellationToken)
    {
        var result = await _mongoRepository.GetAllAsync(cancellationToken);
        return _mapper.FromResponse(result);
    }
}