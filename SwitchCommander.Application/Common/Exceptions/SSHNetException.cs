namespace SwitchCommander.Application.Common.Exceptions;

public class SSHNetException : Exception
{
    public SSHNetException(string message) : base(message)
    {
    }
}