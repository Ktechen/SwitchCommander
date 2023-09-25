using MediatR;
using Microsoft.Extensions.Logging;

namespace SwitchCommander.Application.Features.UserManagement.DomainEvents;

public class CreateUserDomainEventHandler : INotificationHandler<CreateUserDomainEvent>
{
    private readonly ILogger<CreateUserDomainEventHandler> _logger;

    public CreateUserDomainEventHandler(ILogger<CreateUserDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CreateUserDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Call Event !!!!");
        return Task.CompletedTask;
    }
}