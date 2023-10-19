using Hangfire;
using Microsoft.Extensions.Configuration;
using SwitchCommander.Application.Common.Services;
using SwitchCommander.Application.Repositories.Features.SSH;

namespace SwitchCommander.Infrastructure.Features.SSH.BackgroundJobs;

public sealed class CreateInitHangfireTask
{
    private readonly IPingService _pingService;

    public CreateInitHangfireTask(IPingService pingService, ISshServerMongoRepository mongoRepository,
        IConfiguration configuration)
    {
        _pingService = pingService;

        var config = configuration.GetSection("SSHServerPingRequestInterval");

        var readAllServer = mongoRepository.ReadAllAsync().Result;
        foreach (var sshServer in readAllServer)
            BackgroundJob.Schedule(
                () => SendPingServer(sshServer.Hostname),
                new TimeSpan(0, 1, 0));
    }

    private async void SendPingServer(string hostname)
    {
        var result = await _pingService.IsConnected(hostname);
    }
}