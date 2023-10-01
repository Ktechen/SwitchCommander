using MediatR;
using SwitchCommander.Application.Repositories.Features;

namespace SwitchCommander.Application.Features.SSH.UpdateSSHCommand;

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

public class UpdateSSHCommandConfiguration : IRequestHandler<UpdateSSHCommandConfigurationRequest,
    UpdateSSHCommandConfigurationResponse>
{
    private readonly UpdateSSHCommandConfigurationMapper _mapper;
    private readonly ISSHCommandConfigurationRepository _repository;

    public UpdateSSHCommandConfiguration(ISSHCommandConfigurationRepository repository,
        UpdateSSHCommandConfigurationMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UpdateSSHCommandConfigurationResponse> Handle(UpdateSSHCommandConfigurationRequest request,
        CancellationToken cancellationToken)
    {
        var mapper = _mapper.FromRequest(request);
        var result = await _repository.UpdateAsync(mapper, cancellationToken);
        return new UpdateSSHCommandConfigurationResponse(result);
    }
}