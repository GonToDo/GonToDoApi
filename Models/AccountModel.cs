using GonToDoApi.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GonToDoApi.Models;

public class AccountModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("id")]
    public string? Id { get; set; }

    [BsonElement("user_name")] public string? UserName { get; set; }

    [BsonElement("password")] public string? Password { get; set; }

    [BsonElement("full_name")] public string? FullName { get; set; }

    [BsonElement("gender")]
    [BsonRepresentation(BsonType.String)]
    public Enums.Gender? Gender { get; set; } = Enums.Gender.Other;

    [BsonIgnoreIfNull]
    [BsonElement("date_of_birth")]
    public string? DateOfBirth { get; set; }

    [BsonIgnoreIfNull]
    [BsonElement("phone_number")]
    public string? PhoneNumber { get; set; }

    [BsonIgnoreIfNull]
    [BsonElement("email_address")]
    public string? EmailAddress { get; set; }

    [BsonIgnoreIfNull]
    [BsonElement("ip_address")]
    public string? IpAddress { get; set; }

    [BsonElement("date_created")]
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    [BsonIgnoreIfNull]
    [BsonElement("language")]
    public string? Language { get; set; }

    [BsonIgnoreIfNull]
    [BsonElement("old_password")]
    public string? OldPassword { get; set; }
}