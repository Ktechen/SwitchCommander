namespace SwitchCommander.Application.Common.Services;

public interface IPasswordService
{
    public Task<string> HashPassword(string password);
    public Task<bool> VerifyPassword(string password, string hashedPassword);
}