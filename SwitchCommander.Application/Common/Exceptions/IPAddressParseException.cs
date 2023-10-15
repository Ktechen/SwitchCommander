namespace SwitchCommander.Application.Common.Exceptions;

public class IPAddressParseException : Exception
{
    public IPAddressParseException(string message) : base(message)
    {
    }
}
