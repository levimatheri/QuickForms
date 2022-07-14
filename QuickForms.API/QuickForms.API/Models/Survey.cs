using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace QuickForms.API.Models;

public class Survey
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; } = null!;

    [BsonElement("content")]
    public BsonDocument? Content { get; set; }
}
