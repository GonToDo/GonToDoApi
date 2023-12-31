using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GonToDoApi.Core.Elements;

public class Model
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
}