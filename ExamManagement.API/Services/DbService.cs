using ExamManagement.API.Data;
using ExamManagement.API.Models;

namespace ExamManagement.API.Services;

public class DbService : IDbService
{
    private readonly UserDb _userDb;

    public DbService(UserDb userDb)
    {
        _userDb = userDb;
    }

    public async Task<User> CreateUserAsync(
        UserDto request, byte[] passwordHash, byte[] passwordSalt)
    {
        var name = request.Name;
        var email = request.Email;
        var role = request.Role;
        var user = new User(
            name, email, passwordHash, passwordSalt, role);
        await _userDb.CreateUserAsync(user);
        return user;
    }

    public async Task<List<User>> GetUsersAsync()
    {
        var users = await _userDb.GetUsersAsync();
        return users;
    }
}

public interface IDbService
{
    Task<List<User>> GetUsersAsync();
    Task<User> CreateUserAsync(UserDto request, byte[] passwordHash, byte[] passwordSalt);
}
