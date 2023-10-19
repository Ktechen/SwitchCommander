using MediatR;
using Microsoft.Extensions.Logging;
using SwitchCommander.Application.Repositories.Features.SSH;

namespace SwitchCommander.Application.Features.SSH.Create.Sequence;

public sealed record CreateSSHSequenceRequest
    (string? SequenceName, List<Guid>? commands) : IRequest;


public class CreateSSHSequenceHandler : IRequestHandler<CreateSSHSequenceRequest>
{
    private readonly ILogger<CreateSSHSequenceHandler> _logger;
    private readonly ISshCommandSequenceMongoRepository _commandSequenceMongoRepository;
    private readonly ISshCommandMongoRepository _commandMongoRepository;

    public CreateSSHSequenceHandler(ILogger<CreateSSHSequenceHandler> logger, ISshCommandSequenceMongoRepository commandSequenceMongoRepository, ISshCommandMongoRepository commandMongoRepository)
    {
        _logger = logger;
        _commandSequenceMongoRepository = commandSequenceMongoRepository;
        _commandMongoRepository = commandMongoRepository;
    }

    public async Task Handle(CreateSSHSequenceRequest request, CancellationToken cancellationToken)
    {
        var getCommands =
            await _commandMongoRepository.FindAsync(x => request.commands.Contains(x.Id), cancellationToken);
        
    }
}