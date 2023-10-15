using System.Net;

namespace SwitchCommander.Domain.Dtos;

public class StatusInformation
{
    public string IpAddress { get; set; }
    public string Hostname { get; set; }
    public string LastExecutedCommand { get; set; }
}