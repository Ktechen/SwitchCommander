using System.Net;
using System.Net.NetworkInformation;

namespace SwitchCommander.Application.Common.Services;

public class PingService : IPingService
{
    private readonly Ping _ping = new();

    public async Task<bool> IsConnected(string hostname)
    {
        var resultDns = await Dns.GetHostAddressesAsync(hostname);
        var result = await _ping.SendPingAsync(resultDns[0]);
        return result.Status == IPStatus.Success;
    }

    public async Task<PingReply> GetPingReply(string hostname)
    {
        var resultDns = await Dns.GetHostAddressesAsync(hostname); 
        return await _ping.SendPingAsync(resultDns[0]);
    }
}