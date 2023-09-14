using System.Net;
using System.Net.NetworkInformation;

namespace SwitchPrototype.Services;

public class PingService : IPingService
{
    public async Task<bool> IsDevicePingable()
    {
        var ping = new Ping();
        var result = await ping.SendPingAsync(IPAddress.Loopback);
        return result.Status == IPStatus.Success;
    }
}