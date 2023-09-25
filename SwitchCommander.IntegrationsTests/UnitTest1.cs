using Renci.SshNet;

namespace SwitchCommander.IntegrationsTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        using var client = new SshClient("sftp.foo.com", "guest", "pwd");
        client.Connect();
        if (!client.IsConnected) return;

        var resultOfExecuteCommand = client.RunCommand("");

        Assert.Pass();
    }
}