using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbFramework.Abstractions;

namespace SwitchCommander.Domain.Common;

public abstract class BaseEntity : IDocument<Guid>
{
    public DateTimeOffset DateCreated { get; set; }
    public DateTimeOffset? DateUpdated { get; set; }
    public DateTimeOffset? DateDeleted { get; set; }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public Guid Id { get; set; }
}