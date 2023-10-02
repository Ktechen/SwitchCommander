using MediatR;
using SwitchCommander.Application.Features.SSH.Services;
using SwitchCommander.Application.Repositories.Features.SSH;
using SwitchCommander.Domain.Dtos;
using SwitchCommander.Persistence.Context;

namespace SwitchCommander.Persistence.Repositories.Features.SSH;

public class SSHServerRepository : BaseRepository<SSHServer>, ISSHServerRepository
{
    private readonly ISSHNetService _netService;

    public SSHServerRepository(MongoDbContext context, IMediator mediator, ISSHNetService netService) : base(
        context.SSHServerCollection, mediator)
    {
        _netService = netService;
    }

    public async Task<string> ExecuteCommand(SSHServer server, SSHCommand sshCommand)
    {
        return await _netService.RunCommand(server, sshCommand);
    }
}