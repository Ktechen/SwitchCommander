using MediatR;
using Microsoft.Extensions.Logging;
using SwitchCommander.Application.Repositories.Features.SSH;
using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Features.SSH.Create.Sequence;

public sealed record CreateSSHSequenceRequest (string? SequenceName, List<Guid>? Commands) : IRequest<CreateSSHSequenceResponse>;

public sealed record CreateSSHSequenceResponse (bool IsCreated);

public sealed record CreateSSHSequenceDto(string? SequenceName, List<SSHCommand> SshCommands);

public class CreateSSHSequenceHandler : IRequestHandler<CreateSSHSequenceRequest, CreateSSHSequenceResponse>
{
    private readonly ILogger<CreateSSHSequenceHandler> _logger;
    private readonly ISshCommandSequenceMongoRepository _commandSequenceMongoRepository;
    private readonly ISshCommandMongoRepository _commandMongoRepository;
    private readonly CreateSSHSequenceMapper _mapper;

    public CreateSSHSequenceHandler(ILogger<CreateSSHSequenceHandler> logger, ISshCommandSequenceMongoRepository commandSequenceMongoRepository, ISshCommandMongoRepository commandMongoRepository, CreateSSHSequenceMapper mapper)
    {
        _logger = logger;
        _commandSequenceMongoRepository = commandSequenceMongoRepository;
        _commandMongoRepository = commandMongoRepository;
        _mapper = mapper;
    }

    public async Task<CreateSSHSequenceResponse> Handle(CreateSSHSequenceRequest request, CancellationToken cancellationToken)
    {
        var getCommands =
            await _commandMongoRepository.FindAsync(x => request.Commands.Contains(x.Id), cancellationToken);
        
        if (!getCommands.Any())
        {
            return new CreateSSHSequenceResponse(false);
        }

        var dto = new CreateSSHSequenceDto(request.SequenceName, getCommands.ToList());
        var mapper = _mapper.FromDto(dto);
        
        await _commandSequenceMongoRepository.AddAsync(mapper, cancellationToken);
        _logger.LogInformation($"Create SSH Sequence Response - {mapper.SshCommands} ");
        
        return new CreateSSHSequenceResponse(true);
    }
}