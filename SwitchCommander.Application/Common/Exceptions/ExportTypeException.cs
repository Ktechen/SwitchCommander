namespace SwitchCommander.Application.Common.Exceptions;

public class ExportTypeException : Exception
{
    public ExportTypeException()
    {
    }

    public ExportTypeException(string message) : base(message)
    {
    }

    public ExportTypeException(string[] errors) : base("Multiple errors occurred. See error details.")
    {
        Errors = errors;
    }

    public string[] Errors { get; set; }
}