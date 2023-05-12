using ExamManagement.API.Models;
using MongoDB.Driver;

namespace ExamManagement.API.Data;

public class UserDb
{
    private readonly IMongoCollection<User> _user;

    public UserDb(IMongoDatabase db)
    {
        _user = db.GetCollection<User>("Users");
    }

    public async Task<List<User>> GetUsersAsync()
    {
        var users = await _user.Find(u => true).ToListAsync();
        return users;
    }

    public async Task<User> GetUserAsync(string email)
    {
        return await _user.Find(u => u.Email == email).FirstOrDefaultAsync();
    }

    public async Task<User> GetUserByIdAsync(string userId)
    {
        var user = await _user.Find(u => u.Id == userId).FirstOrDefaultAsync();
        return user;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        await _user.InsertOneAsync(user);
        return user;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        await _user.ReplaceOneAsync(u => u.Id == user.Id, user);
        return user;
    }

    public async Task<User> DeleteUserAsync(string userId)
    {
        var user = await _user.FindOneAndDeleteAsync(u => u.Id == userId);
        return user;
    }
}
