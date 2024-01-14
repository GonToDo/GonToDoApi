using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GonToDoApi.Models;

public class TaskModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("id")]
    public string? Id { get; set; }

    [BsonElement("account_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? AccountId { get; set; }

    [BsonElement("checked")] public bool? Checked { get; set; } = false;

    [BsonElement("title")] public string? Title { get; set; }

    [BsonElement("content")] public string? Content { get; set; }

    [BsonElement("date_created")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    [BsonElement("time")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime? Time { get; set; }

    [BsonElement("category_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? CategoryId { get; set; }
}