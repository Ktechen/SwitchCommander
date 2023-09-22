using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SwitchCommander.Domain.Common;

namespace SwitchCommander.Domain.Dtos;

public sealed class User : IBaseEntity
{
    public long DateCreated { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    
    public long? DateUpdated { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    
    public long? DateDeleted { get; set; }

    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    
    public string? Email { get; set; }
    public string? Name { get; set; }

}