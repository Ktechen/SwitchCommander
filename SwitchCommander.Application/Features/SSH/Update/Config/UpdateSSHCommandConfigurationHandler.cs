using MediatR;
using SwitchCommander.Application.Repositories.Features.SSH;

namespace SwitchCommander.Application.Features.SSH.Update.Config;

public sealed record UpdateSSHCommandConfigurationRequest(
    Guid Id,
    int CommandMinimumLength,
    int CommandMaximumLength,
    int DescriptionMinimumLength,
    int DescriptionMaximumLength
) : IRequest<UpdateSSHCommandConfigurationResponse>;

public sealed record UpdateSSHCommandConfigurationResponse(
    bool status
);

public class UpdateSSHCommandConfigurationHandler : IRequestHandler<UpdateSSHCommandConfigurationRequest,
    UpdateSSHCommandConfigurationResponse>
{
    private readonly UpdateSSHCommandConfigurationMapper _mapper;
    private readonly ISshCommandConfigurationMongoRepository _mongoRepository;

    public UpdateSSHCommandConfigurationHandler(ISshCommandConfigurationMongoRepository mongoRepository,
        UpdateSSHCommandConfigurationMapper mapper)
    {
        _mongoRepository = mongoRepository;
        _mapper = mapper;
    }

    public async Task<UpdateSSHCommandConfigurationResponse> Handle(UpdateSSHCommandConfigurationRequest request,
        CancellationToken cancellationToken)
    {
        var mapper = _mapper.FromRequest(request);
        var result = await _mongoRepository.ReplaceAsync(mapper, cancellationToken);
        return new UpdateSSHCommandConfigurationResponse(result);
    }
}