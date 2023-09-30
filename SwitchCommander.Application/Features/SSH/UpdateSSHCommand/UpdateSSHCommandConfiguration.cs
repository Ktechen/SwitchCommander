using MediatR;
using SwitchCommander.Application.Repositories.Features;

namespace SwitchCommander.Application.Features.SSH.UpdateSSHCommand;

public sealed record UpdateSSHCommandConfigurationRequest(
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
    private readonly ISSHCommandConfigurationRepository _repository;
    private readonly UpdateSSHCommandConfigurationMapper _mapper;

    public UpdateSSHCommandConfiguration(ISSHCommandConfigurationRepository repository, UpdateSSHCommandConfigurationMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UpdateSSHCommandConfigurationResponse> Handle(UpdateSSHCommandConfigurationRequest request,
        CancellationToken cancellationToken)
    {
        var mapper = _mapper.FromRequest(request);
        var foundConfig = await _repository.GetAllAsync(cancellationToken);
        if (!foundConfig.Any())
        {
            return new UpdateSSHCommandConfigurationResponse(false);
        }

        var elem = foundConfig.First();
        elem.CommandMaximumLength = mapper.CommandMaximumLength;
        elem.CommandMinimumLength = mapper.CommandMinimumLength;
        elem.DescriptionMaximumLength = mapper.DescriptionMaximumLength;
        elem.DescriptionMinimumLength = mapper.DescriptionMinimumLength;
        
        var result = await _repository.UpdateAsync(elem, cancellationToken);
        return new UpdateSSHCommandConfigurationResponse(result);
    }
}