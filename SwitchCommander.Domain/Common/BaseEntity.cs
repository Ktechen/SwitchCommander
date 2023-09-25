using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SwitchCommander.Domain.Common;

public interface IDomainEvent : INotification
{
}

public abstract class BaseEntity
{
    public long DateCreated { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    public long? DateUpdated { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }

    [BsonIgnore] public List<IDomainEvent> DomainEvents { get; } = new();

    public void QueueDomainEvent(IDomainEvent @event)
    {
        DomainEvents.Add(@event);
    }
}