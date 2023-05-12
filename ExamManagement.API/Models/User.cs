namespace ExamManagement.API.Models;

public class User
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
    public string Role { get; set; } = "Student"; // "Admin" or "Student
    public string UserId { get; set; }

    public User()
    {
        UserId = Guid.NewGuid().ToString();
    }

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
        UserId = Guid.NewGuid().ToString();
    }

    public string GetId()
    {
        return UserId;
    }
}
