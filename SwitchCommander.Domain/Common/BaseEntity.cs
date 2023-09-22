using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SwitchCommander.Domain.Common;

public abstract class BaseEntity 
{
    public long DateCreated { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    public long? DateUpdated { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    public long? DateDeleted { get; set; }
    
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    
}