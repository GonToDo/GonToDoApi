using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GonToDoApi.Models;

public class AccountModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? FullName { get; set; }

    public string? DateOfBirth { get; set; }

    public string? PhoneNumber { get; set; }

    public string? EmailAddress { get; set; }

    public string? IpAddress { get; set; }

    public string? DateCreated { get; set; }

    public string? Language { get; set; }

    [BsonIgnoreIfNull] public string? OldPassword { get; set; }
}