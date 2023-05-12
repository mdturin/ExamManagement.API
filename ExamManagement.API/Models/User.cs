using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExamManagement.API.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
    public string Role { get; set; } = "Student"; // "Admin" or "Student

    public User(
        string name, 
        string email, 
        byte[] passwordHash, 
        byte[] passwordSalt, 
        string role = "Student")
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
        Role = role;
    }
}
