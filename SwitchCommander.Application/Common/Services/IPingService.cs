using System.Net.NetworkInformation;

namespace SwitchCommander.Application.Common.Services;

public interface IPingService
{
    public Task<bool> IsConnected(string hostname);
    public Task<PingReply> GetPingReply(string hostname);
}