namespace SwitchCommander.Application.Common.Services;

public class PasswordService : IPasswordService
{
    private readonly string pepper = "";
    
    public async Task<string> HashPassword(string password)
    {
        var pepperedPassword = pepper + password;
        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(pepperedPassword, salt);
        return await Task.FromResult(hashedPassword);
    }
    
    public async Task<bool> VerifyPassword(string password, string hashedPassword)
    {
        return await Task.FromResult(BCrypt.Net.BCrypt.Verify(pepper + password, hashedPassword));
    }
}