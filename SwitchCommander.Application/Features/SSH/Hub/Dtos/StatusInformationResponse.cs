namespace SwitchCommander.Application.Features.SSH.Hub.Dtos;

public record StatusInformationResponse(string IpAddress, string Hostname, string LastExecutedCommand);