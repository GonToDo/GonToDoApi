using GonToDoApi.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GonToDoApi.Models;

public class CategoryModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("id")]
    public string? Id { get; set; }

    [BsonElement("account_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? AccountId { get; set; }

    [BsonElement("name")] public string? Name { get; set; }

    [BsonElement("color")] public Enums.CategoryColor Color { get; set; } = Enums.CategoryColor.Red;
}