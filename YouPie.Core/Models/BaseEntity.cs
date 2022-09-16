using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace YouPie.Core.Models;

public abstract class BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}