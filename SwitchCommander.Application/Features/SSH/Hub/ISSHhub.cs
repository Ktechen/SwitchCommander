using SwitchCommander.Application.Features.SSH.Hub.Dtos;

namespace SwitchCommander.Application.Features.SSH;

public interface ISSHhub
{
    public Task<StatusInformationResponse> GetStatusInformation(string hostname);
}