﻿using MediatR;
using Microsoft.Extensions.Logging;
using SwitchCommander.Domain.Common;

namespace SwitchCommander.Application.Features.User.CreateUser;

public record CreateUserDomainEvent(Domain.Dtos.User? User) : IDomainEvent;

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