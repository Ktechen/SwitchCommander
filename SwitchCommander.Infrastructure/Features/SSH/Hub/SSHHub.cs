using Microsoft.Extensions.Logging;

namespace SwitchCommander.Infrastructure.Features.SSH.Hub;

public class SSHHub : Microsoft.AspNet.SignalR.Hub
{
    private readonly ILogger<SSHHub> _logger;
    
    public SSHHub(ILogger<SSHHub> logger)
    {
        _logger = logger;
    }
    
    public async Task SendIsConnected(bool result)
    {
        await Clients.All.SendAsync("IsConnected", result);
    }


    public override Task OnDisconnected(bool stopCalled)
    {
        _logger.LogInformation("OnDisconnected SSHhub");
        return base.OnDisconnected(stopCalled);
    }

    public override Task OnConnected()
    {
        _logger.LogInformation("OnConnected SSHhub");
        return base.OnConnected();
    }

    public override Task OnReconnected()
    {
        _logger.LogInformation("OnReconnected SSHhub");
        return base.OnReconnected();
    }

}