using Renci.SshNet;
using Xunit.Abstractions;

namespace SwitchCommander.IntegrationTests.Features.SSH.Services;

public class SSHNetServiceTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public SSHNetServiceTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Connect_To_Rasperrypi_SSH_Server()
    {
        using var client = new SshClient("raspberrypi", "kevin", "1234");
        client.Connect();
        client.Disconnect();
        var result = client.RunCommand("ls");
        //var command = $"echo {1234} | sudo -S apt-get install net-tools";
        var command = "netstat -taupen";
        using (var cmd = client.RunCommand(command))
        {
            if (cmd.ExitStatus == 0)
            {
                var cmdResult = cmd.Result;
                _testOutputHelper.WriteLine(cmd.Result);
            }
            else
            {
                var cmdError = cmd.Error;
                _testOutputHelper.WriteLine(cmd.Error);
            }
        }

        Assert.Contains("text", result.Result);
    }
}