using MediatR;
using Microsoft.Extensions.Logging;
using SwitchCommander.Application.Repositories.Features.SSH;

namespace SwitchCommander.Application.Features.SSH.Read.Command;

public sealed record ReadSSHCommandRequest(Guid Id) : IRequest<ReadSSHCommandResponse>;

public sealed record ReadSSHCommandResponse(Guid? Id, string? Name, string? Description, string? Command,
    long DateCreated, long? DateUpdated);

public class ReadSSHCommandHandler : IRequestHandler<ReadSSHCommandRequest, ReadSSHCommandResponse>
{
    private readonly ILogger<ReadSSHCommandHandler> _logger;
    private readonly ReadSSHCommandMapper _mapper;
    private readonly ISshCommandMongoRepository _mongoRepository;

    public ReadSSHCommandHandler(ILogger<ReadSSHCommandHandler> logger, ISshCommandMongoRepository mongoRepository,
        ReadSSHCommandMapper mapper)
    {
        _logger = logger;
        _mongoRepository = mongoRepository;
        _mapper = mapper;
    }

    public async Task<ReadSSHCommandResponse> Handle(ReadSSHCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _mongoRepository.FindByIdAsync(request.Id, cancellationToken);
        if (result is null) return new ReadSSHCommandResponse(null, null, null, null, 0, 0);

        _logger.LogInformation("Read by Id: {0} result: {1}", request.Id, result);
        var map = _mapper.ToResponse(result);
        return map;
    }
}