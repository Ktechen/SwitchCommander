using SwitchCommander.Domain.Common;
using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Features.UserManagement.DomainEvents;

public class CreateUserDomainEvent : IDomainEvent
{
    public User User { get; }

    public CreateUserDomainEvent(User user)
    {
        User = user;
    }
}