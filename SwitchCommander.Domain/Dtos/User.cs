using SwitchCommander.Domain.Common;

namespace SwitchCommander.Domain.Dtos;

public sealed class User : BaseEntity
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public bool IsEmailConfirmed { get; set; }

    public string Token { get; set; }
    public bool IsTokenExpired { get; set; }

    public bool IsLockedOut { get; set; } // Account Lockout
    public DateTime? LockoutEndDate { get; set; } // Account Lockout
    public int AccessFailedCount { get; set; } // Account Lockout
}