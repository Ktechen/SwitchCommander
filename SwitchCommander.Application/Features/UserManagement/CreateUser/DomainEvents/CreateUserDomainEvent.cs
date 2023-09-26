using SwitchCommander.Domain.Common;
using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Features.UserManagement.CreateUser.DomainEvents;

public class CreateUserDomainEvent : IDomainEvent
{
    public CreateUserDomainEvent(User user)
    {
        User = user;
    }

    public User User { get; }
}