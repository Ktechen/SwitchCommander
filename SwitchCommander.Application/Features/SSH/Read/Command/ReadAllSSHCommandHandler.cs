using MediatR;
using Microsoft.Extensions.Logging;
using SwitchCommander.Application.Repositories.Features.SSH;

namespace SwitchCommander.Application.Features.SSH.Read.Command;

public sealed record ReadAllSSHCommandRequest() : IRequest<IEnumerable<ReadAllSSHCommandResponse>>;

public sealed record ReadAllSSHCommandResponse(Guid? Id, string? Name, string? Description, string? Command,
    long DateCreated, long? DateUpdated);

public class ReadAllSSHCommandHandler : IRequestHandler<ReadAllSSHCommandRequest, IEnumerable<ReadAllSSHCommandResponse>>
{
    private readonly ILogger<ReadAllSSHCommandHandler> _logger;
    private readonly ReadSSHCommandMapper _mapper;
    private readonly ISshCommandMongoRepository _mongoRepository;

    public ReadAllSSHCommandHandler(ILogger<ReadAllSSHCommandHandler> logger, ISshCommandMongoRepository mongoRepository,
        ReadSSHCommandMapper mapper)
    {
        _logger = logger;
        _mongoRepository = mongoRepository;
        _mapper = mapper;
    }


    public async Task<IEnumerable<ReadAllSSHCommandResponse>> Handle(ReadAllSSHCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _mongoRepository.ReadAllAsync(cancellationToken);
        var map = _mapper.ToResponse(result);
        return map;
    }
}