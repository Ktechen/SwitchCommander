﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Domain.Common;

public abstract class BaseEntity
{
    public long DateCreated { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    public long? DateUpdated { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; } = Guid.NewGuid();

    [BsonIgnore] public List<IDomainEvent> DomainEvents { get; } = new();

    public void QueueDomainEvent(IDomainEvent @event)
    {
        DomainEvents.Add(@event);
    }
}