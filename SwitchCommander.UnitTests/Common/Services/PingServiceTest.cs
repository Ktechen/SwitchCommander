using System.Net.NetworkInformation;
using System.Net.Sockets;
using SwitchCommander.Application.Common.Services;

namespace SwitchCommander.UnitTests.Common.Services;

public class PingServiceTest
{
    [Fact]
    public async Task IsConnected_HostnameIsValid_ReturnsTrue()
    {
        // Arrange
        var pingService = new PingService();
        var validHostname = "example.com";

        // Act
        var isConnected = await pingService.IsConnected(validHostname);

        // Assert
        Assert.True(isConnected);
    }

    [Fact]
    public async Task IsConnected_HostnameIsInvalid_ReturnsFalse()
    {
        // Arrange
        var pingService = new PingService();
        var invalidHostname = "invalid-hostname";
        
        // Assert
        await Assert.ThrowsAsync<SocketException>(async () => await pingService.IsConnected(invalidHostname));
    }

    [Fact]
    public async Task GetPingReply_HostnameIsValid_ReturnsPingReply()
    {
        // Arrange
        var pingService = new PingService();
        var validHostname = "example.com";

        // Act
        var pingReply = await pingService.GetPingReply(validHostname);

        // Assert
        Assert.NotNull(pingReply);
        Assert.Equal(IPStatus.Success, pingReply.Status);
    }

    [Fact]
    public async Task GetPingReply_HostnameIsInvalid_ReturnsNull()
    {
        // Arrange
        var pingService = new PingService();
        var invalidHostname = "invalid-hostname";
        
        // Assert
        await Assert.ThrowsAsync<SocketException>(async () => await pingService.GetPingReply(invalidHostname));
    }
}